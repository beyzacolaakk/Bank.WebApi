using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface ISubeServis
    {
        IDataResult<List<Sube>> HepsiniGetir();

        IResult Ekle(Sube sube);

        IResult Guncelle(Sube sube);

        IResult Sil(Sube sube);

        IDataResult<Sube> IdIleGetir(int id); 
    }
}
