using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface ISubeServis
    {
       Task<IDataResult<List<Sube>>> HepsiniGetir();

        Task<IResult> Ekle(Sube sube);

        Task<IResult> Guncelle(Sube sube);

        Task<IResult> Sil(Sube sube);

        Task<IDataResult<Sube>> IdIleGetir(int id);

        Task<IDataResult<List<SubeDto>>> SubeGetir();
    }
}
