using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Soyut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class HesapServis : IHesapServis
    {
        private readonly IHesapDal _hesapDal;

        public HesapServis(IHesapDal hesapDal)
        {
            _hesapDal = hesapDal;
        }

        public async Task<IResult> Ekle(Hesap hesap)
        {
            await _hesapDal.Ekle(hesap);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(Hesap hesap)
        {
            await _hesapDal.Guncelle(hesap);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(Hesap hesap)
        {
            await _hesapDal.Sil(hesap);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
        public async Task<IResult> OtomatikHesapOlustur(HesapOlusturDto hesapOlusturDto)
        {
            var hesap = new Hesap
            {
                KullaniciId = hesapOlusturDto.KullaniciId,
                HesapNo = HesapNoUret(),
                HesapTipi = hesapOlusturDto.HesapTipi,
                Bakiye = 0,
                ParaBirimi = hesapOlusturDto.ParaBirimi,
                OlusturmaTarihi = DateTime.Now
            };

            await _hesapDal.Ekle(hesap);
            return new SuccessResult("Hesap başarıyla oluşturuldu.");
        }
        public async Task<IResult> ParaTransferi(int gonderenHesapId, int aliciHesapId, decimal miktar)
        {
            if (miktar <= 0)
                return new ErrorResult("Transfer miktarı sıfırdan büyük olmalıdır.");
             
            if (gonderenHesapId == aliciHesapId)
                return new ErrorResult("Kendinize para transferi yapamazsınız.");

            // Hesapları paralel al (performans için)
            var gonderenTask = HesapNoIdIleGetir(gonderenHesapId);
            var aliciTask = HesapNoIdIleGetir(aliciHesapId);

            await Task.WhenAll(gonderenTask, aliciTask);

            var gonderen = gonderenTask.Result;
            var alici = aliciTask.Result;

            if (!gonderen.Success || gonderen.Data == null)
                return new ErrorResult("Gönderen hesap bulunamadı.");

            if (!alici.Success || alici.Data == null)
                return new ErrorResult("Alıcı hesap bulunamadı.");

            if (gonderen.Data.ParaBirimi != alici.Data.ParaBirimi)
                return new ErrorResult("Hesaplar farklı para birimi kullanıyor.");

            if (gonderen.Data.Bakiye < miktar)
                return new ErrorResult("Gönderen hesapta yeterli bakiye yok.");

            try
            {
  
                gonderen.Data.Bakiye -= miktar;
                alici.Data.Bakiye += miktar;

                await _hesapDal.Guncelle(gonderen.Data);
                await _hesapDal.Guncelle(alici.Data);

        

                return new SuccessResult("Para transferi başarılı.");
            }
            catch (Exception ex)
            {
      
                return new ErrorResult($"Transfer sırasında bir hata oluştu: {ex.Message}");
            }
        }

        private int HesapNoUret()
        {
            var random = new Random();
            return random.Next(100000, 1000000); 
        }
        public async Task<IDataResult<List<Hesap>>> HepsiniGetir()
        {
            var hesaplar = await _hesapDal.HepsiniGetir();
            return new SuccessDataResult<List<Hesap>>(hesaplar, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<Hesap>> IdIleGetir(int id)
        {
            var hesap = await _hesapDal.Getir(h => h.Id == id);
            return new SuccessDataResult<Hesap>(hesap, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<IDataResult<Hesap>> HesapNoIdIleGetir(int id) 
        {
            var hesap = await _hesapDal.Getir(h => h.HesapNo == id);
            return new SuccessDataResult<Hesap>(hesap, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<Hesap> KullaniciHesapGetirIlkVadesiz(int kullaniciId)
        {
            var sonuclar= await _hesapDal.HepsiniGetir(h => h.KullaniciId == kullaniciId); 
            return sonuclar.FirstOrDefault(h => h.HesapTipi == "Vadesiz")!;

        }
    }

}
