using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Soyut;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class GirisOlayiServis : IGirisOlayiServis
    {
        private readonly IGirisOlayiDal _girisOlayiDal;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "GirisOlayiListesi";
        public GirisOlayiServis(IGirisOlayiDal girisOlayiDal, IMemoryCache memoryCache)
        {
            _girisOlayiDal = girisOlayiDal;
            _memoryCache = memoryCache;
        }

        public async Task<IResult> Ekle(GirisOlayi girisOlayi)
        {
            await _girisOlayiDal.Ekle(girisOlayi);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(GirisOlayi girisOlayi)
        {
            await _girisOlayiDal.Guncelle(girisOlayi);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(GirisOlayi girisOlayi)
        {
            await _girisOlayiDal.Sil(girisOlayi);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }

        public async Task<IDataResult<List<GirisOlayi>>> HepsiniGetir(string sortBy = "Zaman", bool desc = false)
        {

            if (_memoryCache.TryGetValue(CacheKey, out List<GirisOlayi> cachedListe))
            {
         
                var sortedList = desc
                    ? cachedListe.OrderByDescending(x => x.Zaman).ToList()
                    : cachedListe.OrderBy(x => x.Zaman).ToList();

                return new SuccessDataResult<List<GirisOlayi>>(sortedList, Mesajlar.HepsiniGetirmeBasarili);
            }

  
            var liste = await _girisOlayiDal.HepsiniGetir();


            _memoryCache.Set(CacheKey, liste, TimeSpan.FromMinutes(5));


            var sorted = desc
                ? liste.OrderByDescending(x => x.Zaman).ToList()
                : liste.OrderBy(x => x.Zaman).ToList();

            return new SuccessDataResult<List<GirisOlayi>>(sorted, Mesajlar.HepsiniGetirmeBasarili);
        }


        public async Task<IDataResult<GirisOlayi>> IdIleGetir(int id)
        {
            var girisOlayi = await _girisOlayiDal.Getir(x => x.Id == id);
            return new SuccessDataResult<GirisOlayi>(girisOlayi, Mesajlar.IdIleGetirmeBasarili);
        }
    }

}
