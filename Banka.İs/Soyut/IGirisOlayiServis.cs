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
        IDataResult<List<GirisOlayi>> HepsiniGetir();

        IResult Ekle(GirisOlayi girisOlayi); 

        IResult Guncelle(GirisOlayi girisOlayi); 

        IResult Sil(GirisOlayi girisOlayi);  

        IDataResult<GirisOlayi> IdIleGetir(int id);  
    }
}
