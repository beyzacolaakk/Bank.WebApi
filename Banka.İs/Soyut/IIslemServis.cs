using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IIslemServis
    {
        IDataResult<List<Islem>> HepsiniGetir();

        IResult Ekle(Islem islem); 

        IResult Guncelle(Islem islem);

        IResult Sil(Islem islem); 

        IDataResult<Islem> IdIleGetir(int id);
    }
}
