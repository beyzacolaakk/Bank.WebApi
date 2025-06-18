using Banka.İs.Sabitler;
using Banka.İs.Somut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Soyut;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Test.BirimTestleri
{
    public class KartServisTests
    {
        private readonly Mock<IKartDal> _kartDalMock;
        private readonly KartServis _kartServis;

        public KartServisTests()
        {
            _kartDalMock = new Mock<IKartDal>();
            _kartServis = new KartServis(_kartDalMock.Object);
        }

        [Fact]
        public async Task KartIstekleriGetir_Basarili()
        {
          
            var fakeList = new List<KartIstekleriDto>
        {
            new KartIstekleriDto {  },
            new KartIstekleriDto {  }
        };

            _kartDalMock.Setup(k => k.KartIstekleriGetir()).ReturnsAsync(fakeList);

        
            var result = await _kartServis.KartIstekleriGetir();

    
            Assert.True(result.Success);
            Assert.Equal(fakeList, result.Data);
            Assert.Equal(Mesajlar.HepsiniGetirmeBasarili, result.Message);
        }

        [Fact]
        public async Task Ekle_Calistiginda_SuccessResultDonmeli()
        {
         
            var yeniKart = new Kart();

            _kartDalMock.Setup(k => k.Ekle(yeniKart)).Returns(Task.CompletedTask);

      
            var result = await _kartServis.Ekle(yeniKart);

       
            Assert.True(result.Success);
            Assert.Equal(Mesajlar.EklemeBasarili, result.Message);
            _kartDalMock.Verify(k => k.Ekle(yeniKart), Times.Once);
        }

        [Fact]
        public async Task ParaCekYatir_ParaYatirma_Basarili()
        {
       
            var kart = new Kart { Id = 1, Limit = 1000m };
            var dto = new ParaCekYatirDto { HesapId = "1234", IslemTipi = "Para Yatırma", Tutar = 200 };

            _kartDalMock.Setup(k => k.Getir(It.IsAny<System.Linq.Expressions.Expression<System.Func<Kart, bool>>>()))
                        .ReturnsAsync(kart);

            _kartDalMock.Setup(k => k.Guncelle(It.IsAny<Kart>())).Returns(Task.CompletedTask);

      
            var result = await _kartServis.ParaCekYatir(dto);

      
            Assert.True(result.Success);
            Assert.Equal(1200m, result.Data);
            Assert.Equal("Para transferi başarılı.", result.Message);
        }

        [Fact]
        public async Task ParaCekYatir_GonderenKartYok_ErrorDonmeli()
        {
        
            var dto = new ParaCekYatirDto { HesapId = "1234", IslemTipi = "Para Yatırma", Tutar = 200 };

            _kartDalMock.Setup(k => k.Getir(It.IsAny<System.Linq.Expressions.Expression<System.Func<Kart, bool>>>()))
                        .ReturnsAsync((Kart)null);

    
            var result = await _kartServis.ParaCekYatir(dto);

       
            Assert.False(result.Success);
            Assert.Equal("Gönderen hesap bulunamadı.", result.Message);
        }

        [Fact]
        public async Task ParaCekYatir_ParaCekme_BakiyeYetersiz_ErrorDonmeli()
        {
      
            var kart = new Kart { Id = 1, Limit = 100m };
            var dto = new ParaCekYatirDto { HesapId = "1234", IslemTipi = "Para Çekme", Tutar = 200 };

            _kartDalMock.Setup(k => k.Getir(It.IsAny<System.Linq.Expressions.Expression<System.Func<Kart, bool>>>()))
                        .ReturnsAsync(kart);

     
            var result = await _kartServis.ParaCekYatir(dto);

          
            Assert.False(result.Success);
            Assert.Equal("Gönderen hesapta yeterli bakiye yok.", result.Message);
        }
    }
}
