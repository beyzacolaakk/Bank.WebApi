using Banka.İs.Somut;
using Banka.Varlıklar.Somut;
using Banka.Varlıklar.DTOs;
using Banka.VeriErisimi.Soyut;
using Banka.VeriErisimi.Somut.EntityFramework;
using Banka.Cekirdek.YardımcıHizmetler.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Banka.İs.Soyut;
using Banka.VeriErisim.Somut.EntityFramework;

namespace Banka.Test.EntegrasyonTestleri
{
    public class KartIslemServisEntegrasyonTest
    {
        private readonly KartIslemServis _servis;
        private readonly BankaContext _context;
        private readonly IKartIslemDal _kartIslemDal;
        private readonly IKartServis _kartServis;
        private readonly IHesapServis _hesapServis;
        private readonly IKartDal _kartDal;
        private readonly IHesapDal _hesapDal;
        public KartIslemServisEntegrasyonTest()
        {

            var options = new DbContextOptionsBuilder<BankaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BankaContext(options);

         
            _kartIslemDal = new EfKartIslemDal(_context);  
            _kartServis = new KartServis(_kartDal); 
            _hesapServis = new HesapServis(_hesapDal, _kartServis);

            _servis = new KartIslemServis(_kartIslemDal, _hesapServis, _kartServis);

           
        }





        [Fact]
        public async Task HepsiniGetir_BosDegil_ListDonmeli()
        {
            _context.KartIslemleri.Add(new KartIslem { Aciklama = "İşlem 1", Durum = "Başarılı", GuncelBakiye = 1000, IslemTarihi = DateTime.Now, IslemTuru = "Ödeme", Tutar = 100 });
            _context.KartIslemleri.Add(new KartIslem { Aciklama = "İşlem 2", Durum = "Başarılı", GuncelBakiye = 900, IslemTarihi = DateTime.Now, IslemTuru = "Tahsilat", Tutar = 50 });
            await _context.SaveChangesAsync();

            var result = await _servis.HepsiniGetir();

            Assert.True(result.Success);
            Assert.NotEmpty(result.Data);
            Assert.True(result.Data.Count >= 2);
        }



    }
}