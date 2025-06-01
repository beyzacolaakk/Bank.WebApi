using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Güvenlik.Hashing;
using Banka.Cekirdek.YardımcıHizmetler.Güvenlik.JWT;
using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Soyut;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class AuthServis : IAuthServis
    {

         private IKullaniciServis _kullaniciServis; 
        private ITokenHelper _tokenHelper;
        private IKullaniciRolServis _kullaniciRolServis;   
        private IGirisOlayiServis _girisOlayiServis;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthServis(IKullaniciServis kullaniciServis, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor,IKullaniciRolServis kullaniciRolServis, IGirisOlayiServis girisOlayiServis) 
        {
            _kullaniciServis = kullaniciServis;
            _tokenHelper = tokenHelper;
            _kullaniciRolServis = kullaniciRolServis;
            _girisOlayiServis = girisOlayiServis;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IDataResult<AccessToken>> ErisimTokenOlustur(IDataResult<Kullanici> kullanici) 
        {
            var claims = await _kullaniciServis.YetkileriGetir(kullanici.Data);
            var accessToken =  _tokenHelper.TokenOlustur(kullanici.Data, claims);

            return new SuccessDataResult<AccessToken>(accessToken, kullanici.Message);
        }

        public async Task<IDataResult<Kullanici>> Giris(KullaniciGirisDto kullaniciGirisDto)
        {
            var kontrolEdilenTelefon = await _kullaniciServis.MaileGoreGetir(kullaniciGirisDto.Telefon);  
            if (kontrolEdilenTelefon == null)
            {
                return new ErrorDataResult<Kullanici>(Mesajlar.KullanıcıBulunamadı);
            }

            if (!HashingHelper.HashSifreDogrula(kullaniciGirisDto.Sifre, kontrolEdilenTelefon.SifreHash, kontrolEdilenTelefon.SifreSalt))
            {
                return new ErrorDataResult<Kullanici>(Mesajlar.HatalıGiris); 
            }

            return new SuccessDataResult<Kullanici>(kontrolEdilenTelefon, Mesajlar.GirisBasarili);
        }
        public async Task<IDataResult<AccessToken>> KayitIslemi(KullaniciKayitDto kullaniciKayitDto)
        {
            var kullaniciMevcut =await  KullaniciMevcut(kullaniciKayitDto.Telefon);
            if (!kullaniciMevcut.Success)
                return new ErrorDataResult<AccessToken>(kullaniciMevcut.Message);

            var kayitSonuc = await  Kayit(kullaniciKayitDto, kullaniciKayitDto.Sifre);
            if (!kayitSonuc.Success)
                return new ErrorDataResult<AccessToken>(kayitSonuc.Message);

            var tokenSonuc = await ErisimTokenOlustur(kayitSonuc);
            if (!tokenSonuc.Success)
                return new ErrorDataResult<AccessToken>(tokenSonuc.Message);

            var rolSonuc = await KullaniciRolEkle(kayitSonuc);
            if (!rolSonuc.Success)
                return new ErrorDataResult<AccessToken>(rolSonuc.Message);

            return new SuccessDataResult<AccessToken>(tokenSonuc.Data, tokenSonuc.Message);
        }

        public async Task<IDataResult<Kullanici>> Kayit(KullaniciKayitDto kullaniciKayitDto, string sifre)
        {
            byte[] sifreHash, sifreSalt; 
            HashingHelper.HashSifreOlustur(sifre, out sifreHash, out sifreSalt);   


          
                var kullanici = new Kullanici 
                {
                    Email = kullaniciKayitDto.Email,
                    AdSoyad = kullaniciKayitDto.AdSoyad,
                    SifreHash = sifreHash,
                    SifreSalt = sifreSalt,
                    KayitTarihi = DateTime.UtcNow,
                    Aktif= true,
                    Telefon=kullaniciKayitDto.Telefon,
                    SubeId=kullaniciKayitDto.Sube,
                   
                };

                var number = await _kullaniciServis.Ekle(kullanici);
                if(number.Success)
                {
                    return new SuccessDataResult<Kullanici>(kullanici, Mesajlar.KullaniciEklemeBasarili);
                }

           
            return new ErrorDataResult<Kullanici>(Mesajlar.KullaniciEklemeBasarisiz);
        }
        public async Task<IDataResult<KullaniciVeTokenDto>> GirisVeTokenOlustur(KullaniciGirisDto kullaniciGirisDto)
        {
            var kullaniciGiris = await Giris(kullaniciGirisDto);
            if (!kullaniciGiris.Success)
            {
                // Başarısız giriş, logla ve dön
                var girisOlayiBasarisiz = new GirisOlayi
                {
                    KullaniciId = 0, // kullanıcı yoksa 0 veya -1 verilebilir
                    IpAdresi = GetClientIp(),
                    Basarili = false,
                    Zaman = DateTime.Now
                };
                await _girisOlayiServis.Ekle(girisOlayiBasarisiz);

                return new ErrorDataResult<KullaniciVeTokenDto>(kullaniciGiris.Message);
            }

            var tokenResult = await ErisimTokenOlustur(kullaniciGiris);
            if (!tokenResult.Success)
            {
                var girisOlayiBasarisizToken = new GirisOlayi
                {
                    KullaniciId = kullaniciGiris.Data.Id,
                    IpAdresi = GetClientIp(),
                    Basarili = false,
                    Zaman = DateTime.Now
                };
                await _girisOlayiServis.Ekle(girisOlayiBasarisizToken);

                return new ErrorDataResult<KullaniciVeTokenDto>(tokenResult.Message);
            }

            var kullaniciBilgisi = _kullaniciServis.IdIleGetir(kullaniciGiris.Data.Id);
            if (kullaniciBilgisi == null)
            {
                return new ErrorDataResult<KullaniciVeTokenDto>("Kullanıcı bilgileri bulunamadı.");
            }

            var girisOlayi = new GirisOlayi
            {
                KullaniciId = kullaniciGiris.Data.Id,
                IpAdresi = GetClientIp(),
                Basarili = true,
                Zaman = DateTime.Now
            };
            await _girisOlayiServis.Ekle(girisOlayi);

            var kullaniciVeTokenDto = new KullaniciVeTokenDto
            {
                Token = tokenResult.Data
            };

            return new SuccessDataResult<KullaniciVeTokenDto>(kullaniciVeTokenDto, "Giriş ve token oluşturma başarılı.");
        }

        public async Task<IResult> KullaniciMevcut(string email)
        {
            if (await _kullaniciServis.MaileGoreGetir(email) != null)
            {
                return new ErrorResult(Mesajlar.ZatenVar);
            }
            return new SuccessResult();
        }

        public async Task<IResult> KullaniciRolEkle(IDataResult<Kullanici> kullanici)
        {
            var kullaniciRol = new KullaniciRol
            {
                KullaniciId = kullanici.Data.Id,
                RolId = 1
            };
            var result = await _kullaniciRolServis.Ekle(kullaniciRol);
            if (!result.Success)
            { 
                new ErrorResult(Mesajlar.HataliEkleme);

            }
            return new SuccessResult();
        }
        private string GetClientIp()
        {
            var forwardedHeader = _httpContextAccessor.HttpContext?.Request?.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!string.IsNullOrEmpty(forwardedHeader))
                return forwardedHeader;

            return _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
        }
    }
}
