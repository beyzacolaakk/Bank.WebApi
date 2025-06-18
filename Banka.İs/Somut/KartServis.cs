using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class KartServis : IKartServis
    {
        private readonly IKartDal _kartDal;

        public KartServis(IKartDal kartDal)
        {
            _kartDal = kartDal;

        }
        public async Task<IDataResult<List<KartIstekleriDto>>> KartIstekleriGetir()
        {
            var kartIstekleri = await _kartDal.KartIstekleriGetir();
            return new SuccessDataResult<List<KartIstekleriDto>>(kartIstekleri, Mesajlar.HepsiniGetirmeBasarili);
        }


        public async Task<IResult> Ekle(Kart kart)
        {
            
            await _kartDal.Ekle(kart);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(Kart kart)
        {
            await _kartDal.Guncelle(kart);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }
        public async Task<IResult> OtomatikKartOlustur(KartOlusturDto kartOlusturDto)
        {
            var kart = new Kart
            {
                KullaniciId = kartOlusturDto.KullaniciId,
                KartNumarasi = KartNumarasiUret(),
                CVV = CvvUret(),
                KartTipi = kartOlusturDto.KartTipi,
                SonKullanma = DateTime.UtcNow.AddYears(3),
                Limit = kartOlusturDto.KartTipi == "Kredi Kartı" ? 5000 : (int?)null,
                Durum= "Beklemede",
            };

            await _kartDal.Ekle(kart);
            return new SuccessResult("Kart otomatik olarak oluşturuldu.");
        }
        public async Task<IDataResult<List<Kart>>> HepsiniGetir()
        {
            var veriler = await _kartDal.HepsiniGetir();
            return new SuccessDataResult<List<Kart>>(veriler, Mesajlar.HepsiniGetirmeBasarili);
        }
        public async Task<IDataResult<KartIstekleriDto>> KartIstekleriIdIleGetir(int id) 
        {
            var kartIstekleri = await _kartDal.KartIstekleriGetirIdIle(id); 
            return new SuccessDataResult<KartIstekleriDto>(kartIstekleri, Mesajlar.BasariliGetirme);
        }
        public async Task<IDataResult<Kart>> IdIleGetir(int id)
        {
            var veri = await _kartDal.Getir(k => k.Id == id);
            return new SuccessDataResult<Kart>(veri, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<IDataResult<Kart>> KartNoIleGetir(string id) 
        {
            var veri = await _kartDal.Getir(k => k.KartNumarasi == id);
            return new SuccessDataResult<Kart>(veri, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<List<int>> GetirKullaniciyaAitKartIdler(int kullaniciId)
        {
            return new List<int>(_kartDal.GetirKullaniciyaAitKartIdler(kullaniciId));
        }

        public async Task<IDataResult<List<Kart>>> IdIleHepsiniGetir(int id)
        {
            var veri = await _kartDal.HepsiniGetir(k => k.KullaniciId == id && k.Durum != "Beklemede" && k.Durum != "Reddedildi");
            return new SuccessDataResult<List<Kart>>(veri, Mesajlar.IdIleGetirmeBasarili);
        }


        public async Task<IResult> Sil(Kart kart)
        {
            await _kartDal.Sil(kart);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
        private string KartNumarasiUret()
        {
            var random = new Random();
            // 16 haneli rastgele kart numarası
            return string.Concat(Enumerable.Range(0, 16).Select(_ => random.Next(0, 10).ToString()));
        }

        private string CvvUret()
        {
            var random = new Random();
            // 3 haneli CVV
            return random.Next(100, 1000).ToString();
        }
        public async Task<IDataResult<List<KartDto>>> GetKartlarByKullaniciIdAsync(int kullaniciId)
        { 
            var veri=await _kartDal.GetKartlarByKullaniciIdAsync(kullaniciId); 
            return new SuccessDataResult<List<KartDto>>(veri, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<IDataResult<bool>> KartLimitGuncelle(int kartId,decimal yenlimit)   
        {
            var veri = await _kartDal.KartLimitGuncelle(kartId,yenlimit);
            return new SuccessDataResult<bool>(veri);
        }
        public async Task<IDataResult<decimal>> ParaCekYatir(ParaCekYatirDto paraCekYatirDto)
        {
            var gonderenTask = KartNoIleGetir(paraCekYatirDto.HesapId.ToString());



            var gonderen = gonderenTask.Result;

            if (!gonderen.Success || gonderen.Data == null)
                return new ErrorDataResult<decimal>("Gönderen hesap bulunamadı.");






            try
            {
                if (paraCekYatirDto.IslemTipi == "Para Çekme")
                {
                    if (gonderen.Data.Limit< paraCekYatirDto.Tutar)
                        return new ErrorDataResult<decimal>("Gönderen hesapta yeterli bakiye yok.");
                    gonderen.Data.Limit -= paraCekYatirDto.Tutar;
                }
                else if (paraCekYatirDto.IslemTipi == "Para Yatırma")
                {
                    gonderen.Data.Limit += paraCekYatirDto.Tutar;
                }


                await _kartDal.Guncelle(gonderen.Data);



                return new SuccessDataResult<decimal>(gonderen.Data.Limit.Value, "Para transferi başarılı.");
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<decimal>($"Transfer sırasında bir hata oluştu: {ex.Message}");
            }
        }
        public async Task<IResult> KartDurumGuncelle(DurumuGuncelleDto durumuGuncelleDto) 
        {
            var veri = await _kartDal.KartDurumGuncelle(durumuGuncelleDto.Id!.Value, durumuGuncelleDto.Durum!);
            if (veri)
            {
                return new SuccessResult(Mesajlar.GuncellemeBasarili);
            }
            else
            {
                return new ErrorResult(Mesajlar.GuncellemeBasarisiz);
            }
        }
    }

}
