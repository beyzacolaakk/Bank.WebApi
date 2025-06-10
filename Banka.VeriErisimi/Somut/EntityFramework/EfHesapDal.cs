using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.VeriErisimi.EntityFramework;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisim.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.VeriErisimi.Somut.EntityFramework
{
    public class EfHesapDal : EfEntityRepositoryBase<Hesap, BankaContext>, IHesapDal
    {
        public async Task<List<HesapDto>> GetHesaplarByKullaniciIdAsync(int kullaniciId)
        {
            using var context = new BankaContext();
            return await context.Hesaplar
                .Where(h => h.KullaniciId == kullaniciId)
                .Select(h => new HesapDto
                {
                    HesapTipi = h.HesapTipi,
                    ParaBirimi = h.ParaBirimi,
                    Bakiye = h.Bakiye
                })
                .ToListAsync();
        }
        public List<int> GetirKullaniciyaAitHesapIdler(int kullaniciId)
        { 
            using (var context = new BankaContext())
            {
                return context.Hesaplar
                  .Where(k => k.KullaniciId == kullaniciId)
                  .Select(k => k.Id)
                  .ToList();
            }


        }
    }
}
