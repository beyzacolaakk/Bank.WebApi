using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IGirisOlayiServis
    {
        Task<IDataResult<List<GirisOlayi>>> HepsiniGetir(string sortBy = "Zaman", bool desc = false);

        Task<IResult> Ekle(GirisOlayi girisOlayi);

        Task<IResult> Guncelle(GirisOlayi girisOlayi);

        Task<IResult> Sil(GirisOlayi girisOlayi);  

        Task<IDataResult<GirisOlayi>> IdIleGetir(int id);  
    }
}
