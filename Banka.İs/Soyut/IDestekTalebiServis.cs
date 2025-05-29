using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IDestekTalebiServis
    {
        IDataResult<List<DestekTalebi>> HepsiniGetir(); 

        IResult Ekle(DestekTalebi destekTalebi); 

        IResult Guncelle(DestekTalebi destekTalebi);  

        IResult Sil(DestekTalebi destekTalebi);  

        IDataResult<DestekTalebi> IdIleGetir(int id); 

    }
}
