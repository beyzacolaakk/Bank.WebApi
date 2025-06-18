using Banka.Cekirdek.VeriErisimi;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.VeriErisimi.Soyut
{
    public interface IKartDal : IEntityRepository<Kart>
    {
        Task<List<KartDto>> GetKartlarByKullaniciIdAsync(int kullaniciId);

        List<int> GetirKullaniciyaAitKartIdler(int kullaniciId);

        Task<List<KartIstekleriDto>> KartIstekleriGetir();
        Task<bool> KartLimitGuncelle(int kartId, decimal yeniLimit);
        Task<bool> KartDurumGuncelle(int Id, string yeniDurum);

        Task<KartIstekleriDto?> KartIstekleriGetirIdIle(int id);
    }
}
