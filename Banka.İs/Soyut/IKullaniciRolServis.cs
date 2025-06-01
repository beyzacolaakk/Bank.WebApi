using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IKullaniciRolServis
    {
        Task<IDataResult<List<KullaniciRol>>> HepsiniGetir();

        Task<IResult> Ekle(KullaniciRol kullaniciRol);

        Task<IResult> Guncelle(KullaniciRol kullaniciRol);

        Task<IResult> Sil(KullaniciRol kullaniciRol);

        Task<IDataResult<KullaniciRol>> IdIleGetir(int id);
    }
}
