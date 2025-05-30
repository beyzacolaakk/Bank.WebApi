using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Güvenlik.Hashing;
using Banka.Cekirdek.YardımcıHizmetler.Güvenlik.JWT;
using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Soyut;
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

        public AuthServis(IKullaniciServis kullaniciServis, ITokenHelper tokenHelper,IKullaniciRolServis kullaniciRolServis) 
        {
            _kullaniciServis = kullaniciServis;
            _tokenHelper = tokenHelper;
            _kullaniciRolServis = kullaniciRolServis;
        }
        public IDataResult<AccessToken> ErisimTokenOlustur(IDataResult<Kullanici> kullanici) 
        {
            var claims = _kullaniciServis.YetkileriGetir(kullanici.Data);
            var accessToken = _tokenHelper.TokenOlustur(kullanici.Data, claims);

            return new SuccessDataResult<AccessToken>(accessToken, kullanici.Message);
        }

        public IDataResult<Kullanici> Giris(KullaniciGirisDto kullaniciGirisDto)
        {
            var kontrolEdilenTelefon = _kullaniciServis.MaileGoreGetir(kullaniciGirisDto.Telefon);  
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
        public IDataResult<AccessToken> KayitIslemi(KullaniciKayitDto kullaniciKayitDto)
        {
            var kullaniciMevcut = KullaniciMevcut(kullaniciKayitDto.Telefon);
            if (!kullaniciMevcut.Success)
                return new ErrorDataResult<AccessToken>(kullaniciMevcut.Message);

            var kayitSonuc = Kayit(kullaniciKayitDto, kullaniciKayitDto.Sifre);
            if (!kayitSonuc.Success)
                return new ErrorDataResult<AccessToken>(kayitSonuc.Message);

            var tokenSonuc = ErisimTokenOlustur(kayitSonuc);
            if (!tokenSonuc.Success)
                return new ErrorDataResult<AccessToken>(tokenSonuc.Message);

            var rolSonuc = KullaniciRolEkle(kayitSonuc);
            if (!rolSonuc.Success)
                return new ErrorDataResult<AccessToken>(rolSonuc.Message);

            return new SuccessDataResult<AccessToken>(tokenSonuc.Data, tokenSonuc.Message);
        }

        public IDataResult<Kullanici> Kayit(KullaniciKayitDto kullaniciKayitDto, string sifre)
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

                var number = _kullaniciServis.Ekle(kullanici);
                if(number.Success)
                {
                    return new SuccessDataResult<Kullanici>(kullanici, Mesajlar.KullaniciEklemeBasarili);
                }

           
            return new ErrorDataResult<Kullanici>(Mesajlar.KullaniciEklemeBasarisiz);
        }
        public IDataResult<KullaniciVeTokenDto> GirisVeTokenOlustur(KullaniciGirisDto kullaniciGirisDto)
        {
            var kullaniciGiris = Giris(kullaniciGirisDto);
            if (!kullaniciGiris.Success)
            {
                return new ErrorDataResult<KullaniciVeTokenDto>(kullaniciGiris.Message);
            }

            var tokenResult = ErisimTokenOlustur(kullaniciGiris);
            if (!tokenResult.Success)
            {
                return new ErrorDataResult<KullaniciVeTokenDto>(tokenResult.Message);
            }

            var kullaniciBilgisi = _kullaniciServis.IdIleGetir(kullaniciGiris.Data.Id);
            if (kullaniciBilgisi == null)
            {
                return new ErrorDataResult<KullaniciVeTokenDto>("Kullanıcı bilgileri bulunamadı.");
            }

            var kullaniciVeTokenDto = new KullaniciVeTokenDto
            {
                Token = tokenResult.Data
            };

            return new SuccessDataResult<KullaniciVeTokenDto>(kullaniciVeTokenDto, "Giriş ve token oluşturma başarılı.");
        }
        public IResult KullaniciMevcut(string email)
        {
            if (_kullaniciServis.MaileGoreGetir(email) != null)
            {
                return new ErrorResult(Mesajlar.ZatenVar);
            }
            return new SuccessResult();
        }

        public IResult KullaniciRolEkle(IDataResult<Kullanici> kullanici)
        {
            var kullaniciRol = new KullaniciRol
            {
                KullaniciId = kullanici.Data.Id,
                RolId = 1
            };
            var result = _kullaniciRolServis.Ekle(kullaniciRol);
            if (!result.Success)
            { 
                new ErrorResult(Mesajlar.HataliEkleme);

            }
            return new SuccessResult();
        }
    }
}
