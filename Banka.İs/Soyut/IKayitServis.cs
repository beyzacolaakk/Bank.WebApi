using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IKayitServis
    {
        IDataResult<List<Kayit>> HepsiniGetir();

        IResult Ekle(Kayit kayit);

        IResult Guncelle(Kayit kayit);

        IResult Sil(Kayit kayit);

        IDataResult<Kayit> IdIleGetir(int id);
    }
}
