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
    public interface IKartIslemServis
    {
        Task<IDataResult<List<KartIslem>>> HepsiniGetir(); 

        Task<IResult> Ekle(KartIslem kartIslem);

        Task<IResult> Guncelle(KartIslem kartIslem);

        Task<IResult> Sil(KartIslem kartIslem);

        Task<IDataResult<KartIslem>> IdIleGetir(int id); 
        Task<IResult> KartIleIslemYap(KartIleIslemDto kartIleIslemDto);

        Task<IDataResult<List<SonHareketlerDto>>> KullaniciyaAitSon4KartIslemiGetir(int kullaniciId);
    }
}
