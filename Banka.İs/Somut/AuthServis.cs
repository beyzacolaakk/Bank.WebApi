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
using Microsoft.Extensions.Caching.Memory;
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
        private IGirisTokenServis _girisTokenServis;

        private readonly IMemoryCache _memoryCache;
        public AuthServis(IKullaniciServis kullaniciServis, IGirisTokenServis girisTokenServis, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor,IKullaniciRolServis kullaniciRolServis, IGirisOlayiServis girisOlayiServis, IMemoryCache memoryCache) 
        {
            _kullaniciServis = kullaniciServis;
            _tokenHelper = tokenHelper;
            _kullaniciRolServis = kullaniciRolServis;
            _girisOlayiServis = girisOlayiServis;
            _httpContextAccessor = httpContextAccessor;
            _girisTokenServis = girisTokenServis;
            _memoryCache = memoryCache; 
        }
        public async Task<IDataResult<AccessToken>> ErisimTokenOlustur(IDataResult<Kullanici> kullanici) 
        {
            var claims = await _kullaniciServis.YetkileriGetir(kullanici.Data);
            var accessToken =  _tokenHelper.TokenOlustur(kullanici.Data, claims);
            await _girisTokenServis.Ekle(new GirisToken
            {
                KullaniciId = kullanici.Data.Id,
                GecerlilikBitis= accessToken.Expiration,
                Token=accessToken.Token,
                OlusturmaTarihi = DateTime.UtcNow,

            });
       
            return new SuccessDataResult<AccessToken>(accessToken, kullanici.Message);
        }


        public async Task<IResult> KayitIslemi(KullaniciKayitDto dto)
        {
   
            var mevcutMu = await KullaniciMevcut(dto.Telefon);
            if (!mevcutMu.Success)
                return new ErrorDataResult<AccessToken>(mevcutMu.Message);

   
            HashingHelper.HashSifreOlustur(dto.Sifre, out byte[] sifreHash, out byte[] sifreSalt);

    
            var yeniKullanici = new Kullanici
            {
                Email = dto.Email,
                AdSoyad = dto.AdSoyad,
                Telefon = dto.Telefon,
                SubeId = dto.Sube,
                SifreHash = sifreHash,
                SifreSalt = sifreSalt,
                KayitTarihi = DateTime.UtcNow,
                Aktif = true
            };

 
            var eklemeSonucu = await _kullaniciServis.Ekle(yeniKullanici);
            if (!eklemeSonucu.Success)
                return new ErrorDataResult<AccessToken>(Mesajlar.KullaniciEklemeBasarisiz);


            var rolSonucu = await KullaniciRolEkle(new SuccessDataResult<Kullanici>(yeniKullanici));
            if (!rolSonucu.Success)
                return new ErrorDataResult<AccessToken>(rolSonucu.Message);


            return new SuccessResult(Mesajlar.KayitBasarili);
        }
        public void Cikis(int id)
        {
            _memoryCache.Remove($"kullanici_{id}");

        }
        public async Task<IDataResult<KullaniciVeTokenDto>> GirisVeTokenOlustur(KullaniciGirisDto kullaniciGirisDto)
        {
            var kullanici = await _kullaniciServis.TelefonaGoreGetir(kullaniciGirisDto.Telefon);
            if (kullanici == null)
            {
           

                return new ErrorDataResult<KullaniciVeTokenDto>(Mesajlar.BilgilerHatalı);
            }

            var sifreDogruMu = HashingHelper.HashSifreDogrula(
                kullaniciGirisDto.Sifre,
                kullanici.SifreHash,
                kullanici.SifreSalt
            );

            if (!sifreDogruMu)
            {
                await GirisTakip(kullanici,false, kullaniciGirisDto.IpAdres);

                return new ErrorDataResult<KullaniciVeTokenDto>(Mesajlar.BilgilerHatalı);
            }

            var tokenResult = await ErisimTokenOlustur(new SuccessDataResult<Kullanici>(kullanici));
            if (!tokenResult.Success)
            {
                await GirisTakip(kullanici,false, kullaniciGirisDto.IpAdres);

                return new ErrorDataResult<KullaniciVeTokenDto>(tokenResult.Message);
            }

            await GirisTakip(kullanici, true, kullaniciGirisDto.IpAdres);

            var dto = new KullaniciVeTokenDto
            {
                Token = tokenResult.Data
            };

            return new SuccessDataResult<KullaniciVeTokenDto>(dto, Mesajlar.GirisBasarili);
        }

        private async Task GirisTakip(Kullanici kullanici,bool durum,string ipadres) 
        {
            _memoryCache.Remove("GirisOlayiListesi");
            await _girisOlayiServis.Ekle(new GirisOlayi
            {
                KullaniciId = kullanici.Id,
                IpAdresi =ipadres,
                Basarili = durum,
                Zaman = DateTime.Now
            });
        }

        public async Task<IResult> KullaniciMevcut(string email)
        {
            if (await _kullaniciServis.TelefonaGoreGetir(email) != null)
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
  
    }
}
