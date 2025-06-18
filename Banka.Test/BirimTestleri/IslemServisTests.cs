using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Somut;
using Banka.İs.Soyut;
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
    public class IslemServisTests
    {
        private readonly Mock<IIslemDal> _islemDalMock;
        private readonly Mock<IHesapServis> _hesapServisMock;
        private readonly Mock<IKartServis> _kartServisMock;
        private readonly Mock<IKartIslemServis> _kartIslemServisMock;

        private readonly IslemServis _islemServis;

        public IslemServisTests()
        {
            _islemDalMock = new Mock<IIslemDal>();
            _hesapServisMock = new Mock<IHesapServis>();
            _kartServisMock = new Mock<IKartServis>();
            _kartIslemServisMock = new Mock<IKartIslemServis>();

            _islemServis = new IslemServis(
                _islemDalMock.Object,
                _hesapServisMock.Object,
                _kartServisMock.Object,
                _kartIslemServisMock.Object);
        }
        [Fact]
        public async Task ParaGonderme_HesapIleBasarili()
        {
            var dto = new ParaGondermeDto
            {
                AliciHesapId = "10",
                GonderenHesapId = "20",
                OdemeAraci = "hesap",
                Tutar = 500,
                Aciklama = "Para gönderme testi",
                IslemTipi = "Havale"
            };

            _hesapServisMock.Setup(x => x.HesapNoIdIleGetir(dto.AliciHesapId))
                .ReturnsAsync(new SuccessDataResult<Hesap>(new Hesap { Id = 10 }));

            _hesapServisMock.Setup(x => x.HesapNoIdIleGetir(dto.GonderenHesapId))
                .ReturnsAsync(new SuccessDataResult<Hesap>(new Hesap { Id = 20 }));

            _hesapServisMock.Setup(x => x.ParaTransferi(dto.GonderenHesapId, dto.AliciHesapId, dto.Tutar))
                .ReturnsAsync(new SuccessDataResult<decimal>(1000));

            _islemDalMock.Setup(x => x.Ekle(It.IsAny<Islem>()))
                .Returns(Task.CompletedTask);

            var result = await _islemServis.ParaGonderme(dto);

            Assert.True(result.Success);
            Assert.Equal(Mesajlar.ParaBasariilegonde, result.Message);
        }



        [Fact]
        public async Task KullaniciyaAitSon4KartIslemiGetir_Basarili()
        {
            int kullaniciId = 5;
            var hesapIdler = new List<int> { 1, 2, 3 };

            _hesapServisMock.Setup(x => x.GetirKullaniciyaAitHesapIdler(It.IsAny<int>()))
                .ReturnsAsync(hesapIdler);


            var islemler = new List<Islem>
        {
            new Islem { Aciklama = "İşlem 1", Durum = "Başarılı", GuncelBakiye = 900, IslemTipi = "Havale", IslemTarihi = DateTime.Now, Tutar = 100 },
            new Islem { Aciklama = "İşlem 2", Durum = "Başarılı", GuncelBakiye = 800, IslemTipi = "EFT", IslemTarihi = DateTime.Now.AddMinutes(-5), Tutar = 200 }
        };

            _islemDalMock.Setup(x => x.GetirIslemleri(It.IsAny<List<int>>()))
                .Returns(islemler.ToList());



            var result = await _islemServis.KullaniciyaAitSon4KartIslemiGetir(kullaniciId);

            Assert.True(result.Success);
            Assert.NotEmpty(result.Data);
            Assert.True(result.Data.Count <= 4);
        }

        [Fact]
        public async Task ParaCekYatir_KartIleBasarili()
        {
            var dto = new ParaCekYatirDto
            {
                HesapId = "1234",  // kart numarası
                IslemTuru = "kart",
                Tutar = 300,
                IslemTipi = "Para Yatırma"
            };

            _kartServisMock.Setup(x => x.KartNoIleGetir(dto.HesapId))
                .ReturnsAsync(new SuccessDataResult<Kart>(new Kart { Id = 10 }));

            _kartServisMock.Setup(x => x.ParaCekYatir(dto))
                .ReturnsAsync(new SuccessDataResult<decimal>(1300));

            _kartIslemServisMock.Setup(x => x.Ekle(It.IsAny<KartIslem>()))
                .ReturnsAsync(new SuccessResult());

            _islemDalMock.Setup(x => x.Ekle(It.IsAny<Islem>()))
          .Returns(Task.CompletedTask);



            var result = await _islemServis.ParaCekYatir(dto);

            Assert.True(result.Success);
            Assert.Equal(Mesajlar.ParaBasariilegonde, result.Message);  // Mesajı servis mesajıyla aynı yap
        }

    }

}
