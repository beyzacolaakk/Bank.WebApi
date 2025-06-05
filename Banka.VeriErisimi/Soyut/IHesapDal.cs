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
    }
}
