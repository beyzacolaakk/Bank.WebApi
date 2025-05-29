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
        private IKullaniciDal _kullaniciDal;
         private IKullaniciServis _kullaniciServis; 
        private ITokenHelper _tokenHelper;
        private IKullaniciRolServis _kullaniciRolServis;   

        public AuthServis(IKullaniciServis kullaniciServis, ITokenHelper tokenHelper) 
        {
            _kullaniciServis = kullaniciServis;
            _tokenHelper = tokenHelper;
        }
        public IDataResult<AccessToken> ErisimTokenEkle(IDataResult<Kullanici> kullanici)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Kullanici> Giris(KullaniciGirisDto kullaniciGirisDto)
        {
            var kontrolEdilenTelefon = GetByMail(kullaniciGirisDto.Telefon);  
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
        public Kullanici GetByMail(string telefon)
        {
            var result = _kullaniciDal.Getir(u => u.Telefon == telefon); 
            return result;
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
                if (number.Success)
                {
                    return new SuccessDataResult<Kullanici>(kullanici, Mesajlar.KullaniciEklemeBasarili);
                }

           
            return new ErrorDataResult<Kullanici>(Mesajlar.KullaniciEklemeBasarisiz);
        }

        public IResult KullaniciMevcut(string email)
        {
            throw new NotImplementedException();
        }

        public IResult KullaniciRolEkle(IDataResult<Kullanici> kullanici)
        {
            throw new NotImplementedException();
        }
    }
}
