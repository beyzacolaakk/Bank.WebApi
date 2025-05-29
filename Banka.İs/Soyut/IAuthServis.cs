using Banka.Cekirdek.YardımcıHizmetler.Güvenlik.JWT;
using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banka.Varlıklar.DTOs;
using Banka.Cekirdek.Varlıklar.Somut;
namespace Banka.İs.Soyut
{
    public interface IAuthServis  
    {
        IDataResult<Kullanici> Kayit(KullaniciKayitDto kullaniciKayitDto, string sifre);  
        IDataResult<Kullanici> Giris(KullaniciGirisDto kullaniciGirisDto);  
        IResult KullaniciMevcut(string email); 
        IDataResult<AccessToken> ErisimTokenEkle(IDataResult<Kullanici> kullanici);   
        IResult KullaniciRolEkle(IDataResult<Kullanici> kullanici);  
    }
}
