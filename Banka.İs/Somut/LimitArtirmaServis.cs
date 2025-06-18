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
    public class LimitArtirmaServis : ILimitArtirmaServis
    {
        private readonly ILimitArtirmaDal _limitArtirmaDal; 
        private readonly IKartServis _kartServis;
        private readonly IMemoryCache _memoryCache;
        public LimitArtirmaServis(ILimitArtirmaDal limitArtirmaDal,IKartServis kartServis,IMemoryCache memoryCache)
        {
            _limitArtirmaDal = limitArtirmaDal;
            _kartServis = kartServis;
            _memoryCache = memoryCache;
        }

        public async Task<IResult> Ekle(LimitArtirma limitArtirma) 
        {
            _memoryCache.Remove("kartLimitIstekleriCache");
            await _limitArtirmaDal.Ekle(limitArtirma);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }
        public async Task<IResult> LimitArtirmEkle(LimitArtirmaTalepDto limitArtirma)  
        {
            _memoryCache.Remove("kartLimitIstekleriCache");
            var data=await _kartServis.IdIleGetir(limitArtirma.KartId);
            var veri = new LimitArtirma{ 
                BasvuruTarihi=DateTime.Now,
                Durum= "Beklemede",
                MevcutLimit= limitArtirma.MevcutLimit,
                KartId= data.Data.Id,
                TalepEdilenLimit= limitArtirma.YeniLimit,
                
            };
            await _limitArtirmaDal.Ekle(veri);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }
        public async Task<IResult> Guncelle(LimitArtirma limitArtirma)
        {
            _memoryCache.Remove("kartLimitIstekleriCache");
            await _limitArtirmaDal.Guncelle(limitArtirma);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(int id)
        {
            _memoryCache.Remove("kartLimitIstekleriCache");
            var limitArtirma = IdIleGetir(id).Result.Data;
            await _limitArtirmaDal.Sil(limitArtirma);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }

        public async Task<IDataResult<List<LimitArtirma>>> HepsiniGetir()
        {
            var liste = await _limitArtirmaDal.HepsiniGetir();
            return new SuccessDataResult<List<LimitArtirma>>(liste, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<LimitArtirma>> IdIleGetir(int id)
        {
            var girisOlayi = await _limitArtirmaDal.Getir(x => x.Id == id);
            return new SuccessDataResult<LimitArtirma>(girisOlayi, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<IDataResult<List<LimitArtirmaDto>>> KartLimitIstekleriGetir()
        {
            var cacheKey = "kartLimitIstekleriCache";
            var cachedData = _memoryCache.Get<List<LimitArtirmaDto>>(cacheKey);

            if (cachedData != null)
            {
                return new SuccessDataResult<List<LimitArtirmaDto>>(cachedData, Mesajlar.HepsiniGetirmeBasarili);
            }

            var liste = await _limitArtirmaDal.KartLimitIstekleriGetir();
            _memoryCache.Set(cacheKey, liste, TimeSpan.FromMinutes(5));

            return new SuccessDataResult<List<LimitArtirmaDto>>(liste, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<LimitArtirmaDto>> KartLimitIstekleriGetirIdIle(int id) 
        {

            var liste = await _limitArtirmaDal.KartLimitIstekGetirIdIle(id); 
            return new SuccessDataResult<LimitArtirmaDto>(liste, Mesajlar.HepsiniGetirmeBasarili);
        }
        public async Task<IResult> KartLimitIstekGuncelle(LimitArtirmaEkleDto limitArtirmaEkleDto)
        {
            _memoryCache.Remove("kartLimitIstekleriCache");
            if (limitArtirmaEkleDto.Durum == "Onaylandi")
            {
                decimal mevcutLimit = Convert.ToDecimal(limitArtirmaEkleDto.MevcutLimit);

                decimal yuvarlanmisLimit = Math.Ceiling(mevcutLimit / 5000) * 5000;
                limitArtirmaEkleDto.TalepEdilenLimit = (limitArtirmaEkleDto.TalepEdilenLimit) -(yuvarlanmisLimit-(decimal)limitArtirmaEkleDto.MevcutLimit); 
                var kart = await _kartServis.KartNoIleGetir(limitArtirmaEkleDto.KartNo!);
                var guncelleResult = await _kartServis.KartLimitGuncelle(kart.Data.Id, limitArtirmaEkleDto.TalepEdilenLimit);

                if (guncelleResult.Data)
                {
                    var veri = await _limitArtirmaDal.LimitDurumGuncelle(limitArtirmaEkleDto.Id!.Value, limitArtirmaEkleDto.Durum!);
                    if (veri)
                    {
                        return new SuccessResult(Mesajlar.GuncellemeBasarili);
                    }
                    else
                    {
                        return new ErrorResult(Mesajlar.GuncellemeBasarisiz);
                    }
                }
                else
                {
                    return new ErrorResult("Limit güncellemesi başarısız."); // ← eksik olan yer burasıydı
                }
            }
            else if (limitArtirmaEkleDto.Durum == "Reddedildi")
            {
                var veri = await _limitArtirmaDal.LimitDurumGuncelle(limitArtirmaEkleDto.Id!.Value, limitArtirmaEkleDto.Durum!);
                if (veri)
                {
                    return new SuccessResult(Mesajlar.GuncellemeBasarili);
                }
                else
                {
                    return new ErrorResult(Mesajlar.GuncellemeBasarisiz);
                }
            }
            else
            {
                return new ErrorResult(Mesajlar.GuncellemeBasarisiz);
            }
        }
    


    }
}
