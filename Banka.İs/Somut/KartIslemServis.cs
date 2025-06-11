using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class KartIslemServis : IKartIslemServis
    {
        private readonly IKartIslemDal _kartIslemDal;
        private readonly IKartServis _kartServis;
        private readonly IHesapServis _hesapServis; 

        public KartIslemServis(IKartIslemDal kartIslemDal,IHesapServis hesapServis,IKartServis kartServis)
        {
            _kartIslemDal = kartIslemDal;
            _hesapServis = hesapServis;
            _kartServis = kartServis;
        }

        public async Task<IResult> Ekle(KartIslem kartIslem)
        {
            await _kartIslemDal.Ekle(kartIslem);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(KartIslem kartIslem)
        {
            await _kartIslemDal.Guncelle(kartIslem);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IDataResult<List<KartIslem>>> HepsiniGetir()
        {
            var veriler = await _kartIslemDal.HepsiniGetir();
            return new SuccessDataResult<List<KartIslem>>(veriler, Mesajlar.HepsiniGetirmeBasarili);
        }
        public async Task<IResult> KartIleIslemYap(KartIleIslemDto kartIleIslemDto) 
        {
        
            if (kartIleIslemDto.Tutar<= 0) 
                return new ErrorResult("İşlem tutarı 0'dan büyük olmalıdır.");

         
            var kartResult = await _kartServis.IdIleGetir(kartIleIslemDto.KartId);
            if (kartResult == null || kartResult.Data == null)
                return new ErrorResult("Kart bulunamadı.");

            var kart = kartResult.Data;

            switch (kart.KartTipi)
            {
                case "Kredi Kartı":
                    if (kart.Limit is null)
                        return new ErrorResult("Kredi kartı limiti tanımlı değil.");

                    if (kart.Limit < kartIleIslemDto.Tutar)
                        return new ErrorResult("Yetersiz kredi kartı limiti.");

                    kart.Limit -= kartIleIslemDto.Tutar;
                    var krediKartGuncelleme = await _kartServis.Guncelle(kart);
                    if (!krediKartGuncelleme.Success)
                        return new ErrorResult("Kredi kartı güncellenirken hata oluştu.");

                    return new SuccessResult("Kredi kartı işlemi başarılı.");

                case "Banka Kartı":
                    var hesap = await _hesapServis.KullaniciHesapGetirIlkVadesiz(kart.KullaniciId);
                    if (hesap == null)
                        return new ErrorResult("Kullanıcıya ait banka hesabı bulunamadı.");

                    if (hesap.Bakiye < kartIleIslemDto.Tutar)
                        return new ErrorResult("Yetersiz hesap bakiyesi.");

                    hesap.Bakiye -= kartIleIslemDto.Tutar;
                    var bankaHesapGuncelleme = await _hesapServis.Guncelle(hesap);
                    if (!bankaHesapGuncelleme.Success)
                        return new ErrorResult("Hesap güncellenirken hata oluştu.");

                    return new SuccessResult("Banka kartı işlemi başarılı.");

                default:
                    return new ErrorResult("Geçersiz kart tipi.");
            }
        }

        public async Task<IDataResult<KartIslem>> IdIleGetir(int id)
        {
            var veri = await _kartIslemDal.Getir(k => k.Id == id);
            return new SuccessDataResult<KartIslem>(veri, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<IDataResult<List<SonHareketlerDto>>> KullaniciyaAitSon4KartIslemiGetir(int kullaniciId) 
        {
            var kartIdler = await Task.Run(() => _kartServis.GetirKullaniciyaAitKartIdler(kullaniciId));

            if (!kartIdler.Any())
                return new  ErrorDataResult<List<SonHareketlerDto>>();

            var veri = await Task.Run(() =>
                _kartIslemDal.GetirKartIslemleri(kartIdler)
                             .OrderByDescending(i => i.IslemTarihi)
                             .Take(4)
                             .ToList());
            var sonHareketlerListesi = veri.Select(i => new SonHareketlerDto
            {
                Aciklama = i.Aciklama,
                Durum = i.Durum,
                GuncelBakiye = i.GuncelBakiye,
                IslemTipi = i.IslemTuru,
                Tarih = i.IslemTarihi,
                Tutar = i.Tutar,
                
                
            }).ToList();
            return new SuccessDataResult<List<SonHareketlerDto>>(sonHareketlerListesi);
        }
        public async Task<IResult> Sil(KartIslem kartIslem)
        {
            await _kartIslemDal.Sil(kartIslem);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }

}
