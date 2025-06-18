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
    public interface IHesapDal : IEntityRepository<Hesap>
    {
        Task<List<HesapDto>> GetHesaplarByKullaniciIdAsync(int kullaniciId);

        List<int> GetirKullaniciyaAitHesapIdler(int kullaniciId);

        Task<List<HesapIstekleriDto>> HesapIstekleriGetir();

        Task<IstekSayilariDto> IstekSayilariGetir();

        Task<bool> HesapDurumGuncelle(int Id, string yeniDurum);

        Task<HesapIstekleriDto?> HesapIstekleriGetirIdIle(int id);
    }
}
