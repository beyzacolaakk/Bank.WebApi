using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IKartServis
    {
        IDataResult<List<Kart>> HepsiniGetir();

        IResult Ekle(Kart kart);

        IResult Guncelle(Kart kart);

        IResult Sil(Kart kart); 

        IDataResult<Kart> IdIleGetir(int id);
    }
}
