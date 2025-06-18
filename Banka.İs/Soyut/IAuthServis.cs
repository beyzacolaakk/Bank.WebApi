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
        Task<IResult> KullaniciMevcut(string email); 
        Task<IDataResult<AccessToken>> ErisimTokenOlustur(IDataResult<Kullanici> kullanici);    
        Task<IResult> KullaniciRolEkle(IDataResult<Kullanici> kullanici);
        Task<IDataResult<KullaniciVeTokenDto>> GirisVeTokenOlustur(KullaniciGirisDto kullaniciGirisDto);

        Task<IResult> KayitIslemi(KullaniciKayitDto kullaniciKayitDto);

        void Cikis(int id);
    }
}
