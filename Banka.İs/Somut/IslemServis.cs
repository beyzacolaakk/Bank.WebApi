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
    public class IslemServis : IIslemServis
    {
        private readonly IIslemDal _islemDal;
        private readonly IHesapServis _hesapServis;
        private readonly IKartServis _kartServis;
        public IslemServis(IIslemDal islemDal, IHesapServis hesapServis,IKartServis kartServis)
        {
            _islemDal = islemDal;
            _hesapServis = hesapServis;
            _kartServis = kartServis;
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
 
            var gonderenHesap = await _hesapServis.HesapNoIdIleGetir(paraGondermeDto.GonderenHesapId);
            var aliciHesap = await _hesapServis.HesapNoIdIleGetir(paraGondermeDto.AliciHesapId);

            if (gonderenHesap == null || aliciHesap == null)
            {
                return new ErrorResult("Gönderen veya alıcı hesap bulunamadı.");
            }

            var transferSonucu = await _hesapServis.ParaTransferi(
                paraGondermeDto.GonderenHesapId,
                paraGondermeDto.AliciHesapId,
                paraGondermeDto.Tutar
            );

            var islem = new Islem
            {
                Aciklama = paraGondermeDto.Aciklama,
                AliciHesapId = aliciHesap.Data.Id,
                GonderenHesapId = gonderenHesap.Data.Id,
                IslemTarihi = DateTime.Now,
                Tutar = paraGondermeDto.Tutar,
                IslemTipi = paraGondermeDto.IslemTipi,
                Durum = transferSonucu.Success ? "Başarılı Transfer" : "Başarısız Transfer"
            };

            var islemSonucu = await Ekle(islem);

            if (!islemSonucu.Success)
                return new ErrorResult(Mesajlar.IslemKaydedilmedi);

            return transferSonucu.Success
                ? new SuccessResult(Mesajlar.ParaBasariilegonde)
                : new ErrorResult(Mesajlar.ParaGondermeBasarisiz);
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
