using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.VeriErisimi.Soyut;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class KullaniciServis : IKullaniciServis
    {
        private readonly IKullaniciDal _kullaniciDal;
        private readonly IMemoryCache _memoryCache;
        public KullaniciServis(IKullaniciDal kullaniciDal,IMemoryCache memoryCache)
        {
            _kullaniciDal = kullaniciDal;
            _memoryCache= memoryCache;
        }

        public async Task<IResult> Ekle(Kullanici kullanici)
        {
            await _kullaniciDal.Ekle(kullanici);
            return new SuccessResult(Mesajlar.KullaniciEklemeBasarili);
        }

        public async Task<IResult> Sil(Kullanici kullanici)
        {
            await _kullaniciDal.Sil(kullanici);
            return new SuccessResult(Mesajlar.KullaniciSilmeBasarili);
        }

        public async Task<IResult> Guncelle(Kullanici kullanici)
        {
            await _kullaniciDal.Guncelle(kullanici);
            return new SuccessResult(Mesajlar.KullaniciGuncellemeBasarili);
        }

        public async Task<Kullanici> TelefonaGoreGetir(string telefon) 
        {
            return await _kullaniciDal.Getir(u => u.Telefon == telefon);
        }

        public async Task<IDataResult<List<Kullanici>>> HepsiniGetir()
        {
            var kullanicilar = await _kullaniciDal.HepsiniGetir();
            return new SuccessDataResult<List<Kullanici>>(kullanicilar, "Kullanıcılar Getirildi");
        }

        public async Task<IDataResult<Kullanici>> IdIleGetir(int id)
        {
            var kullanici = await _kullaniciDal.Getir(u => u.Id == id);
            return new SuccessDataResult<Kullanici>(kullanici, "Kullanıcı Getirildi");
        }

        public async Task<List<Rol>> YetkileriGetir(Kullanici kullanici)
        {
            return await _kullaniciDal.YetkileriGetir(kullanici);
        }

        public async Task<IDataResult<KullaniciBilgileriDto>> KullaniciBilgileriGetir(int id)
        {
            string cacheKey = $"kullanici_{id}";

            if (!_memoryCache.TryGetValue(cacheKey, out KullaniciBilgileriDto cachedData))
            {
             
                cachedData = await _kullaniciDal.KullaniciGetir(id);

             
                _memoryCache.Set(cacheKey, cachedData, TimeSpan.FromMinutes(5));
            }

            return new SuccessDataResult<KullaniciBilgileriDto>(cachedData, Mesajlar.BasariliGetirme);
        }
    }

}
