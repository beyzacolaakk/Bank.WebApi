using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IKullaniciServis
    {
        IDataResult<List<Kullanici>> HepsiniGetir();

        IResult Ekle(Kullanici kullanici);

        IResult Guncelle(Kullanici kullanici);

        IResult Sil(Kullanici kullanici);

        IDataResult<Kullanici> IdIleGetir(int id); 
    }
}
