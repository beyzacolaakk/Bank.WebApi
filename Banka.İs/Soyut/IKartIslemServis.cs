using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IKartIslemServis
    {
        IDataResult<List<KartIslem>> HepsiniGetir(); 

        IResult Ekle(KartIslem kartIslem); 

        IResult Guncelle(KartIslem kartIslem);

        IResult Sil(KartIslem kartIslem);

        IDataResult<KartIslem> IdIleGetir(int id);
    }
}
