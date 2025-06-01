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
        Task<IDataResult<List<Kayit>>> HepsiniGetir();

        Task<IResult> Ekle(Kayit kayit);

        Task<IResult> Guncelle(Kayit kayit);

        Task<IResult> Sil(Kayit kayit);

        Task<IDataResult<Kayit>> IdIleGetir(int id);
    }
}
