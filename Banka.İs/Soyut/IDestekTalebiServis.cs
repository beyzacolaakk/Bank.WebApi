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
        Task<IDataResult<List<DestekTalebi>>> HepsiniGetir(); 

        Task<IResult> Ekle(DestekTalebi destekTalebi); 

        Task<IResult> Guncelle(DestekTalebi destekTalebi);  

        Task<IResult> Sil(DestekTalebi destekTalebi);  

        Task<IDataResult<DestekTalebi>> IdIleGetir(int id); 

    }
}
