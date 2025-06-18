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

        Task<IDataResult<List<Kart>>> IdIleHepsiniGetir(int id);

        Task<IDataResult<decimal>> ParaCekYatir(ParaCekYatirDto paraCekYatirDto);
        Task<IDataResult<List<KartDto>>> GetKartlarByKullaniciIdAsync(int kullaniciId);

        Task<IDataResult<Kart>> KartNoIleGetir(string id);

        Task<List<int>> GetirKullaniciyaAitKartIdler(int kullaniciId);

        Task<IDataResult<List<KartIstekleriDto>>> KartIstekleriGetir();

        Task<IDataResult<bool>> KartLimitGuncelle(int kartId, decimal yenlimit);
        Task<IResult> KartDurumGuncelle(DurumuGuncelleDto durumuGuncelleDto);

        Task<IDataResult<KartIstekleriDto>> KartIstekleriIdIleGetir(int id);

    }
}
