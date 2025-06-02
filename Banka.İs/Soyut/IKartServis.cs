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
    public interface IKartServis
    {
        Task<IDataResult<List<Kart>>> HepsiniGetir();

        Task<IResult> Ekle(Kart kart);

        Task<IResult> Guncelle(Kart kart);

        Task<IResult> Sil(Kart kart); 

        Task<IDataResult<Kart>> IdIleGetir(int id);

        Task<IResult> OtomatikKartOlustur(KartOlusturDto kartOlusturDto);
    }
}
