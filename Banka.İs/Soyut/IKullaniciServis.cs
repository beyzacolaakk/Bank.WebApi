using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IKullaniciServis
    {
        Task<IDataResult<List<Kullanici>>> HepsiniGetir();

        Task<IResult> Ekle(Kullanici kullanici);

        Task<IResult> Guncelle(Kullanici kullanici);

        Task<IResult> Sil(Kullanici kullanici);

        Task<IDataResult<Kullanici>> IdIleGetir(int id);

        Task<Kullanici> TelefonaGoreGetir(string telefon); 

        Task<List<Rol>> YetkileriGetir(Kullanici kullanici);

        Task<IDataResult<KullaniciBilgileriDto>> KullaniciBilgileriGetir(int id);
    }
}
