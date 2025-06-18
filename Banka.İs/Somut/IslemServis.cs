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
    public class IslemServis : IIslemServis
    {
        private readonly IIslemDal _islemDal;
        private readonly IHesapServis _hesapServis;
        private readonly IKartServis _kartServis;
        private readonly IKartIslemServis _kartIslemServis;
        public IslemServis(IIslemDal islemDal, IHesapServis hesapServis,IKartServis kartServis,IKartIslemServis kartIslemServis)
        {
            _islemDal = islemDal;
            _hesapServis = hesapServis;
            _kartServis = kartServis;
            _kartIslemServis = kartIslemServis;
        }

        public async Task<IResult> Ekle(Islem islem)
        {
            await _islemDal.Ekle(islem);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(Islem islem)
        {
            await _islemDal.Guncelle(islem);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(Islem islem)
        {
            await _islemDal.Sil(islem);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }

        public async Task<IResult> ParaGonderme(ParaGondermeDto paraGondermeDto)
        {
            // Alıcı hesap bilgisi alınır
            var aliciHesapResult = await _hesapServis.HesapNoIdIleGetir(paraGondermeDto.AliciHesapId);
            if (!aliciHesapResult.Success || aliciHesapResult.Data == null)
                return new ErrorResult("Alıcı hesap bulunamadı.");

            Hesap gonderenHesap = null;
            Kart gonderenKart = null;

            if (paraGondermeDto.OdemeAraci == "hesap")
            {
                var gonderenHesapResult = await _hesapServis.HesapNoIdIleGetir(paraGondermeDto.GonderenHesapId);
                if (!gonderenHesapResult.Success || gonderenHesapResult.Data == null)
                    return new ErrorResult("Gönderen hesap bulunamadı.");

                gonderenHesap = gonderenHesapResult.Data;
            }
            else
            {
                var gonderenKartResult = await _kartServis.KartNoIleGetir(paraGondermeDto.GonderenHesapId);
                if (!gonderenKartResult.Success || gonderenKartResult.Data == null)
                    return new ErrorResult("Gönderen kart bulunamadı.");

                gonderenKart = gonderenKartResult.Data;
            }

            // Para transferi işlemi
            var transferSonucu = await _hesapServis.ParaTransferi(
                paraGondermeDto.GonderenHesapId,
                paraGondermeDto.AliciHesapId,
                paraGondermeDto.Tutar
            );

            // Güncel bakiye döndüyse al
            decimal? guncelBakiye = (transferSonucu as IDataResult<decimal>)?.Data;
            if (paraGondermeDto.OdemeAraci == "kart")
            {
                var kartIslem = new KartIslem
                {
                    Aciklama = paraGondermeDto.Aciklama,
                    Durum = transferSonucu.Success ? "Başarılı Transfer" : "Başarısız Transfer",
                    IslemTarihi = DateTime.Now,
                    GuncelBakiye = guncelBakiye!.Value,
                    IslemTuru = paraGondermeDto.IslemTipi,
                    KartId = gonderenKart.Id,
                    Tutar = paraGondermeDto.Tutar,

                };
                var islemkartSonucu = await _kartIslemServis.Ekle(kartIslem);
                if (!islemkartSonucu.Success)
                    return new ErrorResult(Mesajlar.IslemKaydedilmedi);
            }
      
            var islem = new Islem
            {
                Aciklama = paraGondermeDto.Aciklama,
                AliciHesapId = aliciHesapResult.Data.Id,
                GonderenHesapId = gonderenHesap?.Id, // hesap varsa
                 KartId= gonderenKart?.Id,   // kart varsa
                IslemTarihi = DateTime.Now,
                Tutar = paraGondermeDto.Tutar,
                IslemTipi = paraGondermeDto.IslemTipi,
                GuncelBakiye = guncelBakiye,
                Durum = transferSonucu.Success ? "Başarılı Transfer" : "Başarısız Transfer"
            };
          
            var islemSonucu = await Ekle(islem);
            if (!islemSonucu.Success )
                return new ErrorResult(Mesajlar.IslemKaydedilmedi);

            return transferSonucu.Success
                ? new SuccessResult(Mesajlar.ParaBasariilegonde)
                : new ErrorResult(Mesajlar.ParaGondermeBasarisiz);
        }
        public async Task<IDataResult<List<SonHareketlerDto>>> KullaniciyaAitSon4KartIslemiGetir(int kullaniciId)
        {
            var hesapIdler = await Task.Run(() => _hesapServis.GetirKullaniciyaAitHesapIdler(kullaniciId));

            if (!hesapIdler.Any())
                return new ErrorDataResult<List<SonHareketlerDto>>(Mesajlar.BasarisizGetirme);

            var veri = await Task.Run(() =>
                _islemDal.GetirIslemleri(hesapIdler)
                         .OrderByDescending(i => i.IslemTarihi)
                         .Take(4)
                         .ToList());

            var sonHareketlerListesi = veri.Select(i => new SonHareketlerDto
            {
                Aciklama = i.Aciklama,
                Durum = i.Durum,
                GuncelBakiye = i.GuncelBakiye!.Value,
                IslemTipi = i.IslemTipi,
                Tarih = i.IslemTarihi,
                Tutar = i.Tutar
            }).ToList();
            return new SuccessDataResult<List<SonHareketlerDto>>(sonHareketlerListesi, Mesajlar.BasariliGetirme);
        }

        public async Task<IResult> ParaCekYatir(ParaCekYatirDto paraCekYatirDto)
        {
            IResult transferSonucu;
            int? gonderenId = null;

            if (paraCekYatirDto.IslemTuru == "kart")
            {
                var gonderenKart = await _kartServis.KartNoIleGetir(paraCekYatirDto.HesapId);
                if (gonderenKart == null || gonderenKart.Data == null)
                {
                    return new ErrorResult("Gönderen kart bulunamadı.");
                }

                gonderenId = gonderenKart.Data.Id;
                transferSonucu = await _kartServis.ParaCekYatir(paraCekYatirDto);
            }
            else
            {
                var gonderenHesap = await _hesapServis.HesapNoIdIleGetir(paraCekYatirDto.HesapId.ToString());
                if (gonderenHesap == null || gonderenHesap.Data == null)
                {
                    return new ErrorResult("Gönderen hesap bulunamadı.");
                }

                gonderenId = gonderenHesap.Data.Id;
                transferSonucu = await _hesapServis.ParaCekYatir(paraCekYatirDto);
            }
        
            if (gonderenId == null || transferSonucu == null)
            {
                return new ErrorResult("İşlem gerçekleştirilemedi.");
            }

            decimal? guncelBakiye = (transferSonucu as IDataResult<decimal>)?.Data;

            var islem = new Islem
            {
                Aciklama = null,
                IslemTarihi = DateTime.Now,
                Tutar = paraCekYatirDto.Tutar,
                GuncelBakiye = guncelBakiye ?? 0,
                IslemTipi = paraCekYatirDto.IslemTipi,
                
                Durum = transferSonucu.Success ? "Başarılı Transfer" : "Başarısız Transfer"
            };

            if (paraCekYatirDto.IslemTuru == "kart")
            {
                islem.KartId = gonderenId;
                islem.GonderenHesapId = null;
                var kartıslem = new KartIslem
                {
                    Aciklama = null,
                    IslemTarihi = DateTime.Now,
                    KartId = gonderenId.Value,
                    Tutar=paraCekYatirDto.Tutar,
                    GuncelBakiye=guncelBakiye ?? 0,
                    IslemTuru= paraCekYatirDto.IslemTipi,
                    Durum= transferSonucu.Success ? "Başarılı Transfer" : "Başarısız Transfer",
                    


                };
                await _kartIslemServis.Ekle(kartıslem);
            }
            else
            {
                islem.GonderenHesapId = gonderenId;
                islem.KartId = null;
            }

            var islemSonucu = await Ekle(islem);

            if (!islemSonucu.Success)
                return new ErrorResult(Mesajlar.IslemKaydedilmedi);

            return transferSonucu.Success
                ? new SuccessResult(Mesajlar.ParaBasariilegonde)
                : new ErrorResult(Mesajlar.ParaGondermeBasarisiz);
        }

        public async Task<IDataResult<List<Islem>>> HepsiniGetir()
        {
            var islemler = await _islemDal.HepsiniGetir();
            return new SuccessDataResult<List<Islem>>(islemler, Mesajlar.HepsiniGetirmeBasarili);
        }
   
        public async Task<IDataResult<Islem>> IdIleGetir(int id)
        {
            var islem = await _islemDal.Getir(i => i.Id == id);
            return new SuccessDataResult<Islem>(islem, Mesajlar.IdIleGetirmeBasarili);
        }
      
    }

}
