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
        public EfHesapDal(BankaContext context)
        {
        }
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

        public async Task<List<HesapIstekleriDto>> HesapIstekleriGetir()
        {
            using var context = new BankaContext();

            var result = await (from hesap in context.Hesaplar
                                join kullanici in context.Kullanicilar
                                on hesap.KullaniciId equals kullanici.Id
                                select new HesapIstekleriDto
                                {
                                    AdSoyad = kullanici.AdSoyad,
                                    Telefon = kullanici.Telefon,
                                    BasvuruTarihi = hesap.OlusturmaTarihi, // Eğer bu alan varsa
                                    Durum = hesap.Durum,
                                    HesapNo = hesap.HesapNo,
                                    Id = hesap.Id,
                                    Eposta=kullanici.Email,
                                    
                                }).ToListAsync();

            return result;
        }
        public async Task<HesapIstekleriDto?> HesapIstekleriGetirIdIle(int id) 
        {
            using var context = new BankaContext();

            var result = await (from hesap in context.Hesaplar
                                join kullanici in context.Kullanicilar
                                on hesap.KullaniciId equals kullanici.Id
                                where hesap.Id == id
                                select new HesapIstekleriDto
                                {
                                    AdSoyad = kullanici.AdSoyad,
                                    Telefon = kullanici.Telefon,
                                    BasvuruTarihi = hesap.OlusturmaTarihi,
                                    Durum = hesap.Durum,
                                    HesapNo = hesap.HesapNo,
                                    Id = hesap.Id,
                                    Eposta = kullanici.Email
                                }).FirstOrDefaultAsync(); // sadece bir tane döndürür

            return result;
        }



        public async Task<IstekSayilariDto> IstekSayilariGetir()
        {
            using var context = new BankaContext();

            var hesapIstekleri = await context.Hesaplar
                .CountAsync(h => h.Durum == "Beklemede");

            var kartIstekleri = await context.Kartlar
                .CountAsync(k => k.Durum == "Beklemede");

            var destekIstekleri = await context.DestekTalepleri
                .CountAsync(d => d.Durum == "Açık"  );
            var destekIstekleriisl = await context.DestekTalepleri
             .CountAsync(d => d.Durum == "Islemde");

            var limitArtirtma = await context.LimitArtirma
              .CountAsync(d => d.Durum == "Beklemede");

            return new IstekSayilariDto
            {
                HesapIstekleri = hesapIstekleri,
                KartIstekleri = kartIstekleri,
                DestekIstekleri = destekIstekleri + destekIstekleriisl,
                LimitArtirmaIstekleri = limitArtirtma
            };
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
        public async Task<bool> HesapDurumGuncelle(int Id, string yeniDurum) 
        {
            using (var context = new BankaContext())
            {
                var hesapguncelle = await context.Hesaplar.FindAsync(Id); 
                if (hesapguncelle == null)
                {
                    return false; 
                }

                hesapguncelle.Durum = yeniDurum;
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
