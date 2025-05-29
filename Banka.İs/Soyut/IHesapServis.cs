using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IHesapServis
    {
        IDataResult<List<Hesap>> HepsiniGetir();

        IResult Ekle(Hesap hesap); 

        IResult Guncelle(Hesap hesap); 

        IResult Sil(Hesap hesap); 

        IDataResult<Hesap> IdIleGetir(int id);
    }
}
