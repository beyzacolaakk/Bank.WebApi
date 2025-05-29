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
        IDataResult<List<KullaniciRol>> HepsiniGetir();

        IResult Ekle(KullaniciRol kullaniciRol);

        IResult Guncelle(KullaniciRol kullaniciRol);

        IResult Sil(KullaniciRol kullaniciRol);

        IDataResult<KullaniciRol> IdIleGetir(int id);
    }
}
