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
    public class EfKartDal : EfEntityRepositoryBase<Kart, BankaContext>, IKartDal
    {
        public async Task<List<KartDto>> GetKartlarByKullaniciIdAsync(int kullaniciId)
        {
            using (var context = new BankaContext())
            {
                return await context.Kartlar
                    .Where(k => k.KullaniciId == kullaniciId && k.KartTipi == "Kredi Kartı")
                    .Select(h => new KartDto
                    {
                        KartNumarasi = h.KartNumarasi,
                        Limit = h.Limit
                    })
                    .ToListAsync();
            }
   
        }
        public List<int> GetirKullaniciyaAitKartIdler(int kullaniciId)
        {
            using (var context = new BankaContext())
            {
                return context.Kartlar
                  .Where(k => k.KullaniciId == kullaniciId)
                  .Select(k => k.Id)
                  .ToList();
            }
  
   
        }
    }
}
