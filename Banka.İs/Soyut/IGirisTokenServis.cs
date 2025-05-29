using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IGirisTokenServis
    {
        IDataResult<List<GirisToken>> HepsiniGetir(); 

        IResult Ekle(GirisToken girisToken);  

        IResult Guncelle(GirisToken girisToken); 

        IResult Sil(GirisToken girisToken);  

        IDataResult<GirisToken> IdIleGetir(int id); 
    }
}
