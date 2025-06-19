using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Banka.İs.Soyut;
using Banka.İs.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Güvenlik.JWT;
using Banka.Varlıklar.DTOs;
using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Results;
namespace Banka.Test.BirimTestleri
{
    public class AuthServisTests
    {
        private readonly Mock<IKullaniciServis> _kullaniciServisMock;
        private readonly Mock<ITokenHelper> _tokenHelperMock;
        private readonly Mock<IKullaniciRolServis> _kullaniciRolServisMock;
        private readonly Mock<IGirisOlayiServis> _girisOlayiServisMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IGirisTokenServis> _girisTokenServisMock;
        private readonly IMemoryCache _memoryCache;

        private readonly AuthServis _authServis;

        public AuthServisTests()
        {
            _kullaniciServisMock = new Mock<IKullaniciServis>();
            _tokenHelperMock = new Mock<ITokenHelper>();
            _kullaniciRolServisMock = new Mock<IKullaniciRolServis>();
            _girisOlayiServisMock = new Mock<IGirisOlayiServis>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _girisTokenServisMock = new Mock<IGirisTokenServis>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());

            _authServis = new AuthServis(
                _kullaniciServisMock.Object,
                _girisTokenServisMock.Object,
                _tokenHelperMock.Object,
                _httpContextAccessorMock.Object,
                _kullaniciRolServisMock.Object,
                _girisOlayiServisMock.Object,
                _memoryCache
            );
        }

        [Fact]
        public async Task KayitIslemi_KullaniciZatenVarsa_ErrorDonmeli()
        {
            _kullaniciServisMock.Setup(k => k.TelefonaGoreGetir(It.IsAny<string>()))
                .ReturnsAsync(new Kullanici());

            var dto = new KullaniciKayitDto
            {
                Email = "test@test.com",
                Telefon = "05551234567",
                AdSoyad = "Test Kullanıcı",
                Sifre = "123456",
                Sube = 1
            };

            var result = await _authServis.KayitIslemi(dto);

            Assert.False(result.Success);
            Assert.Equal("Zaten Mevcut!", result.Message); 
        }


        [Fact]
        public async Task GirisVeTokenOlustur_BasariliGiris_TokenDonmeli()
        {
            // Arrange
            var kullanici = new Kullanici
            {
                Id = 1,
                Telefon = "05551234567",
                SifreHash = new byte[0],
                SifreSalt = new byte[0]
            };

            _kullaniciServisMock.Setup(k => k.TelefonaGoreGetir(It.IsAny<string>()))
                .ReturnsAsync(kullanici);

            _kullaniciServisMock.Setup(k => k.YetkileriGetir(It.IsAny<Kullanici>()))
                .ReturnsAsync(new List<Rol>
                {
        new Rol { Id = 1, RolAdi = "Müşteri" },
        new Rol { Id = 2, RolAdi = "Yönetici" }
                });

            _tokenHelperMock.Setup(t => t.TokenOlustur(It.IsAny<Kullanici>(), It.IsAny<List<Rol>>()))
                .Returns(new AccessToken { Token = "token", Expiration = DateTime.UtcNow.AddMinutes(30) });


            _girisTokenServisMock.Setup(g => g.Ekle(It.IsAny<GirisToken>()))
                .ReturnsAsync(new SuccessResult()); 


            var girisDto = new KullaniciGirisDto
            {
                Telefon = "05551234567",
                Sifre = "test",
                IpAdres = "127.0.0.1"
            };

  
            var result = await _authServis.GirisVeTokenOlustur(girisDto);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("token", result.Data.Token.Token);
        }
    }
}
