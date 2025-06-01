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
        Task<IDataResult<List<GirisToken>>> HepsiniGetir(); 

        Task<IResult> Ekle(GirisToken girisToken);

        Task<IResult> Guncelle(GirisToken girisToken);

        Task<IResult> Sil(GirisToken girisToken);  

        Task<IDataResult<GirisToken>> IdIleGetir(int id); 
    }
}
