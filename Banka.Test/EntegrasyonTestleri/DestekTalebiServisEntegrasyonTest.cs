using Banka.İs.Sabitler;
using Banka.İs.Somut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisim.Somut.EntityFramework;
using Banka.VeriErisimi.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Test.EntegrasyonTestleri
{
    public class DestekTalebiServisEntegrasyonTest : IDisposable
    {
        private readonly BankaContext _context;
        private readonly DestekTalebiServis _servis;

        public DestekTalebiServisEntegrasyonTest()
        {
            var options = new DbContextOptionsBuilder<BankaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BankaContext(options);
            var destekDal = new EfDestekTalebiDal(_context); 
            _servis = new DestekTalebiServis(destekDal);
        }

   


      



        [Fact]
        public async Task DestekTalebiDurumGuncelle_GecersizId_Ile_BasarisizOlmali()
        {
            var dto = new DestekTalebiGuncelleDto
            {
                Id = 999,
                Durum = "Hatalı",
                Yanit = "Olmadı"
            };

            var result = await _servis.DestekTalebiDurumGuncelle(dto);

            Assert.False(result.Success);
            Assert.Equal(Mesajlar.GuncellemeBasarisiz, result.Message);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

}
