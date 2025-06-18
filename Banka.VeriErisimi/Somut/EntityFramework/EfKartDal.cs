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
        private readonly BankaContext _context;
        public EfKartDal(BankaContext context) 
        {
            _context = context;
        }
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
        public async Task<List<KartIstekleriDto>> KartIstekleriGetir()
        {
            using (var context = new BankaContext())
            {
                var result = await (from kart in context.Kartlar
                                    join kullanici in context.Kullanicilar
                                    on kart.KullaniciId equals kullanici.Id
                                    select new KartIstekleriDto
                                    {
                                        AdSoyad = kullanici.AdSoyad,
                                        Tarih = DateTime.Now,
                                        Durum = kart.Durum,
                                        KartTipi = kart.KartTipi,
                                        Id = kart.Id,
                                        Limit = kart.Limit.HasValue ? kart.Limit.Value : 0 // Null kontrolü
                                    }).ToListAsync();

                return result;
            }
        }
        public async Task<KartIstekleriDto?> KartIstekleriGetirIdIle(int id)
        {
            using (var context = new BankaContext())
            {
                var result = await (from kart in context.Kartlar
                                    join kullanici in context.Kullanicilar
                                    on kart.KullaniciId equals kullanici.Id
                                    where kart.Id == id
                                    select new KartIstekleriDto
                                    {
                                        AdSoyad = kullanici.AdSoyad,
                                        Tarih = DateTime.Now, // İstersen kart.OlusturmaTarihi yapabilirsin
                                        Durum = kart.Durum,
                                        KartTipi = kart.KartTipi,
                                        Id = kart.Id,
                                        Limit = kart.Limit ?? 0
                                    }).FirstOrDefaultAsync();

                return result; // null olabilir, çağıran kontrol etmeli
            }
        }

        public async Task<bool> KartLimitGuncelle(int kartId, decimal yeniLimit)
        {
            using (var context = new BankaContext())
            {
                var kart = await context.Kartlar.FindAsync(kartId);
                if (kart == null)
                {
                    return false; // Kart bulunamadı
                }

                kart.Limit = yeniLimit;
                await context.SaveChangesAsync();
                return true; // Başarıyla güncellendi
            }
        }
        public async Task<bool> KartDurumGuncelle(int Id, string yeniDurum) 
        {
            using (var context = new BankaContext())
            {
                var kartguncelle = await context.Kartlar.FindAsync(Id); 
                if (kartguncelle == null)
                {
                    return false;
                }

                kartguncelle.Durum = yeniDurum;
                await context.SaveChangesAsync();
                return true;
            }
        }





    }
}
