using Banka.Cekirdek.Varlıklar.Somut;
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
    public interface ILimitArtirmaServis
    {
        Task<IDataResult<List<LimitArtirma>>> HepsiniGetir();

        Task<IResult> Ekle(LimitArtirma limitArtirma); 

        Task<IResult> Guncelle(LimitArtirma limitArtirma);

        Task<IResult> Sil(int id); 

        Task<IDataResult<LimitArtirma>> IdIleGetir(int id);

        Task<IDataResult<List<LimitArtirmaDto>>> KartLimitIstekleriGetir();

        Task<IResult> LimitArtirmEkle(LimitArtirmaTalepDto limitArtirma); 

        Task<IResult> KartLimitIstekGuncelle(LimitArtirmaEkleDto limitArtirmaEkleDto);

        Task<IDataResult<LimitArtirmaDto>> KartLimitIstekleriGetirIdIle(int id); 
    }
}
