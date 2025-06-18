using Banka.İs.Somut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisim.Somut.EntityFramework;
using Banka.VeriErisimi.Somut.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Test.EntegrasyonTestleri
{
    public class LimitArtirmaServisEntegrasyonTest
    {
        private readonly BankaContext _context;
        private readonly LimitArtirmaServis _servis;

        public LimitArtirmaServisEntegrasyonTest()
        {
            var options = new DbContextOptionsBuilder<BankaContext>()
                .UseSqlServer("Server=localhost;Database=BankaDb;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;

            _context = new BankaContext(options);

            var limitDal = new EfLimitArtirmaDal(_context);
            var kartServis = new KartServis(new EfKartDal(_context)); 
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            _servis = new LimitArtirmaServis(limitDal, kartServis, memoryCache);
        }

        [Fact]
        public async Task Ekle_GecerliLimitArtirma_Eklenmeli()
        {
            var limit = new LimitArtirma
            {
                KartId = 16, 
                MevcutLimit = 10000,
                TalepEdilenLimit = 20000,
                Durum = "Beklemede",
                BasvuruTarihi = DateTime.Now
            };

            var result = await _servis.Ekle(limit);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task LimitArtirmEkle_GecerliVeri_Eklenmeli()
        {
            var dto = new LimitArtirmaTalepDto
            {
                KartId = 25, 
                MevcutLimit = 15000,
                YeniLimit = 25000
            };

            var result = await _servis.LimitArtirmEkle(dto);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task HepsiniGetir_LimitArtirmaListesi_BasariliOlmali()
        {
            var result = await _servis.HepsiniGetir();

            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 0);
        }

        [Fact]
        public async Task KartLimitIstekleriGetir_CachelemeCalismali()
        {
            var result1 = await _servis.KartLimitIstekleriGetir();
            var result2 = await _servis.KartLimitIstekleriGetir(); 

            Assert.Equal(result1.Data.Count, result2.Data.Count);
        }



        [Fact]
        public async Task KartLimitIstekGuncelle_ReddedildiDurumu_BasariliOlmali()
        {
            var dto = new LimitArtirmaEkleDto
            {
                Id = 30, 
                Durum = "Reddedildi",
                KartNo = "1234567890123456"
            };

            var result = await _servis.KartLimitIstekGuncelle(dto);

            Assert.True(result.Success);
        }
    }
}
