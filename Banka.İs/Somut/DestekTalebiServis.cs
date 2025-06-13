
using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class DestekTalebiServis : IDestekTalebiServis
    {
        private readonly IDestekTalebiDal _destekTalebiDal;
        private readonly IMemoryCache _cache;
        private readonly ILogger<KullaniciServis> _logger;

        public DestekTalebiServis(IDestekTalebiDal destekTalebiDal, IMemoryCache cache, ILogger<KullaniciServis> logger)
        {
            _destekTalebiDal = destekTalebiDal;
            _cache = cache; 
            _logger = logger;   
        }

        public async Task<IResult> Ekle(DestekTalebi destekTalebi)
        {
            await _destekTalebiDal.Ekle(destekTalebi);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(DestekTalebi destekTalebi)
        {
            await _destekTalebiDal.Guncelle(destekTalebi);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(DestekTalebi destekTalebi)
        {
            await _destekTalebiDal.Sil(destekTalebi);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
        public async Task<IResult> DestekTalebiOlustur(DestekTalebiOlusturDto destekTalebiOlusturDto)
        {
            var destekTalebi= new DestekTalebi{
                Durum="Açık",
                Konu=destekTalebiOlusturDto.Konu,
                KullaniciId=destekTalebiOlusturDto.KullaniciId, 
                OlusturmaTarihi=DateTime.Now,
                Mesaj=destekTalebiOlusturDto.Mesaj,
                Kategori=destekTalebiOlusturDto.Kategori
                
            };
            await _destekTalebiDal.Ekle(destekTalebi);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }
        public async Task<IResult> DestekTalebiGuncelle(int id) 
        {
            await _destekTalebiDal.DurumuGuncelle(id, "Tamamlandı"); 
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }
        public async Task<IDataResult<List<DestekTalebi>>> HepsiniGetir()
        {
            string key = "tum_destek_talepleri";

            if (_cache.TryGetValue(key, out List<DestekTalebi> cachedListe))
            {
                _logger.LogInformation("[CACHE] Tüm destek talepleri cache'ten getirildi.");
                return new SuccessDataResult<List<DestekTalebi>>(cachedListe, Mesajlar.HepsiniGetirmeBasarili);
            }

            var liste = await _destekTalebiDal.HepsiniGetir();

            _cache.Set(key, liste, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Priority = CacheItemPriority.Normal
            });

            return new SuccessDataResult<List<DestekTalebi>>(liste, Mesajlar.HepsiniGetirmeBasarili);
        }
        public async Task<IDataResult<List<DestekTalebi>>> IdIleHepsiniGetir(int kullaniciId) 
        {
            var liste = await _destekTalebiDal.HepsiniGetir(x => x.KullaniciId == kullaniciId);
            return new SuccessDataResult<List<DestekTalebi>>(liste, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<IDataResult<List<DestekTalebiOlusturDto>>> DestekIstekleriGetir() 
        {
            var liste = await _destekTalebiDal.DestekTalebleriGetir();
            return new SuccessDataResult<List<DestekTalebiOlusturDto>>(liste, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<IDataResult<DestekTalebi>> IdIleGetir(int id)
        {
            string key = $"destek_talebi_{id}";

            if (_cache.TryGetValue(key, out DestekTalebi cachedenGelen))
            {
                _logger.LogInformation($"[CACHE] Destek Talebi {id} cache'ten getirildi.");
                return new SuccessDataResult<DestekTalebi>(cachedenGelen, Mesajlar.IdIleGetirmeBasarili);
            }

            var destekTalebi = await _destekTalebiDal.Getir(x => x.Id == id);

            if (destekTalebi != null)
            {
                _cache.Set(key, destekTalebi, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(1),
                    Priority = CacheItemPriority.Normal
                });
            }

            return new SuccessDataResult<DestekTalebi>(destekTalebi, Mesajlar.IdIleGetirmeBasarili);
        }
    }


}
