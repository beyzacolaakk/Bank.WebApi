using Banka.İs.Somut;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Soyut;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Test.BirimTestleri
{
    public class HesapServisTests
    {
        private readonly Mock<IHesapDal> _hesapDalMock;
        private readonly Mock<IKartServis> _kartServisMock;
        private readonly HesapServis _hesapServis;

        public HesapServisTests()
        {
            _hesapDalMock = new Mock<IHesapDal>();
            _kartServisMock = new Mock<IKartServis>();
            _hesapServis = new HesapServis(_hesapDalMock.Object, _kartServisMock.Object);
        }

        [Fact]
        public async Task HesapIstekleriGetir_BasariliSonucDonmeli()
        {
         
            var ornekVeri = new List<HesapIstekleriDto>
            {
                new HesapIstekleriDto {  },
                new HesapIstekleriDto {  }
            };
            _hesapDalMock.Setup(dal => dal.HesapIstekleriGetir()).ReturnsAsync(ornekVeri);

            var sonuc = await _hesapServis.HesapIstekleriGetir();

   
            Assert.True(sonuc.Success);
            Assert.Equal(ornekVeri.Count, sonuc.Data.Count);
            _hesapDalMock.Verify(dal => dal.HesapIstekleriGetir(), Times.Once);
        }

        [Fact]
        public async Task OtomatikHesapOlustur_EkleMetodunuCagirirVeBasariliDoner()
        {
      
            var dto = new HesapOlusturDto
            {
                KullaniciId = 1,
                HesapTipi = "Vadesiz"
            };
            _hesapDalMock.Setup(dal => dal.Ekle(It.IsAny<Hesap>())).Returns(Task.CompletedTask);

       
            var sonuc = await _hesapServis.OtomatikHesapOlustur(dto);

          
            Assert.True(sonuc.Success);
            Assert.Contains("başarıyla oluşturuldu", sonuc.Message);
            _hesapDalMock.Verify(dal => dal.Ekle(It.Is<Hesap>(h => h.KullaniciId == dto.KullaniciId && h.HesapTipi == dto.HesapTipi)), Times.Once);
        }

        [Fact]
        public async Task ParaTransferi_MiktarSifirdanKucukVeyaEsitse_HataDonmeli()
        {
      
            decimal miktar = 0;

            var sonuc = await _hesapServis.ParaTransferi("gonderenId", "aliciId", miktar);

    
            Assert.False(sonuc.Success);
            Assert.Equal("Transfer miktarı sıfırdan büyük olmalıdır.", sonuc.Message);
        }

        [Fact]
        public async Task ParaTransferi_AliciHesapBulunamazsa_HataDonmeli()
        {
   
            _hesapDalMock.Setup(dal => dal.Getir(It.IsAny<Expression<Func<Hesap, bool>>>()))
                .ReturnsAsync((Hesap)null);

   
            var sonuc = await _hesapServis.ParaTransferi("gonderenId", "aliciId", 100);

 
            Assert.False(sonuc.Success);
            Assert.Equal("Alıcı hesap bulunamadı.", sonuc.Message);
        }

        [Fact]
        public async Task ParaTransferi_HesaplarArasiTransferBasarili()
        {
    
            var gonderenHesap = new Hesap { Id = 1, Bakiye = 1000, ParaBirimi = "TRY", HesapNo = "gonderenId" };
            var aliciHesap = new Hesap { Id = 2, Bakiye = 500, ParaBirimi = "TRY", HesapNo = "aliciId" };

            _hesapDalMock.Setup(dal => dal.Getir(It.IsAny<Expression<Func<Hesap, bool>>>()))
                .ReturnsAsync((Expression<Func<Hesap, bool>> predicate) =>
                {
                    if (predicate.Compile().Invoke(gonderenHesap)) return gonderenHesap;
                    if (predicate.Compile().Invoke(aliciHesap)) return aliciHesap;
                    return null;
                });

            _hesapDalMock.Setup(dal => dal.Guncelle(It.IsAny<Hesap>())).Returns(Task.CompletedTask);

      
            var sonuc = await _hesapServis.ParaTransferi("gonderenId", "aliciId", 200);

        
            Assert.True(sonuc.Success);
            Assert.Equal(800, gonderenHesap.Bakiye);
            Assert.Equal(700, aliciHesap.Bakiye);
            Assert.Equal(gonderenHesap.Bakiye, sonuc.Data);

            _hesapDalMock.Verify(dal => dal.Guncelle(gonderenHesap), Times.Once);
            _hesapDalMock.Verify(dal => dal.Guncelle(aliciHesap), Times.Once);
        }

        [Fact]
        public async Task ParaCekYatir_HesapBulunamazsa_HataDonmeli()
        {
          
            _hesapDalMock.Setup(dal => dal.Getir(It.IsAny<Expression<Func<Hesap, bool>>>()))
                .ReturnsAsync((Hesap)null);

            var dto = new ParaCekYatirDto { HesapId = "1", Tutar = 100, IslemTipi = "Para Çekme" };


            var sonuc = await _hesapServis.ParaCekYatir(dto);

    
            Assert.False(sonuc.Success);
            Assert.Equal("Gönderen hesap bulunamadı.", sonuc.Message);
        }

        [Fact]
        public async Task ParaCekYatir_YeterliBakiyeVarsa_ParaCekilir()
        {
           
            var hesap = new Hesap { Id = 1, Bakiye = 500 };

            _hesapDalMock.Setup(dal => dal.Getir(It.IsAny<Expression<Func<Hesap, bool>>>()))
                .ReturnsAsync(hesap);

            _hesapDalMock.Setup(dal => dal.Guncelle(It.IsAny<Hesap>())).Returns(Task.CompletedTask);

            var dto = new ParaCekYatirDto
            {
                HesapId = $"{hesap.Id}",
                Tutar = 200,
                IslemTipi = "Para Çekme"
            };

   
            var sonuc = await _hesapServis.ParaCekYatir(dto);


            Assert.True(sonuc.Success);
            Assert.Equal(300, sonuc.Data);
            _hesapDalMock.Verify(dal => dal.Guncelle(It.Is<Hesap>(h => h.Bakiye == 300)), Times.Once);
        }
    }
}
