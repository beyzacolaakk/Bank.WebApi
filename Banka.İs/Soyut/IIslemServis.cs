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
    public interface IIslemServis
    {
        Task<IDataResult<List<Islem>>> HepsiniGetir();

        Task<IResult> Ekle(Islem islem);

        Task<IResult> Guncelle(Islem islem);

        Task<IResult> Sil(Islem islem); 

        Task<IDataResult<Islem>> IdIleGetir(int id);

        Task<IResult> ParaGonderme(ParaGondermeDto paraGondermeDto);

        Task<IResult> ParaCekYatir(ParaCekYatirDto paraCekYatirDto);

        Task<IDataResult<List<SonHareketlerDto>>> KullaniciyaAitSon4KartIslemiGetir(int kullaniciId);
    }
}
