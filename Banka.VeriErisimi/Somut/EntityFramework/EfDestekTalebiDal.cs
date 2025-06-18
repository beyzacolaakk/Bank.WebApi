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
    public class EfDestekTalebiDal : EfEntityRepositoryBase<DestekTalebi, BankaContext>, IDestekTalebiDal 
    {
        public EfDestekTalebiDal(BankaContext context) 
        {
        }
        public async Task DurumuGuncelle(int id, string yeniDurum)
        {
            using (var context = new BankaContext())
            {
                var entity = new DestekTalebi { Id = id };
                context.DestekTalepleri.Attach(entity);
                entity.Durum = yeniDurum;
                context.Entry(entity).Property(x => x.Durum).IsModified = true;
                await context.SaveChangesAsync();
            }
       
        }
        public async Task<bool> DestekTalebiDurumGuncelle(int Id, string yeniDurum, string yanit) 
        {
            using (var context = new BankaContext())
            {
                var destektalebiguncelle = await context.DestekTalepleri.FindAsync(Id);
                if (destektalebiguncelle == null) 
                {
                    return false;
                }

                destektalebiguncelle.Durum = yeniDurum;
                destektalebiguncelle.Yanit = yanit;
                await context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<List<DestekTalebiOlusturDto>> DestekTalebleriGetir()
        {
            using (var context = new BankaContext())
            {
                var result = await (from destek in context.DestekTalepleri
                                    join kullanici in context.Kullanicilar
                                    on destek.KullaniciId equals kullanici.Id
                                    select new DestekTalebiOlusturDto
                                    {
                                        Tarih = destek.OlusturmaTarihi,
                                        AdSoyad = kullanici.AdSoyad,
                                        Durum = destek.Durum,
                                        Kategori = destek.Kategori,
                                        Konu = destek.Konu,
                                        Mesaj = destek.Mesaj,
                                        KullaniciId=kullanici.Id,
                                        Id = destek.Id,
                                        Yanit=destek.Yanit,
                                    }).ToListAsync();

                return result;
            }
        }

        public async Task<DestekTalebiOlusturDto?> DestekTalebiGetirIdIle(int id)
        {
            using (var context = new BankaContext())
            {
                var result = await (from destek in context.DestekTalepleri
                                    join kullanici in context.Kullanicilar
                                    on destek.KullaniciId equals kullanici.Id
                                    where destek.Id == id
                                    select new DestekTalebiOlusturDto
                                    {
                                        Tarih = destek.OlusturmaTarihi,
                                        AdSoyad = kullanici.AdSoyad,
                                        Durum = destek.Durum,
                                        Kategori = destek.Kategori,
                                        Konu = destek.Konu,
                                        Mesaj = destek.Mesaj,
                                        KullaniciId = kullanici.Id,
                                        Id = destek.Id,
                                        Yanit = destek.Yanit,
                                    }).FirstOrDefaultAsync();

                return result; // null olabilir, kontrolü çağıran tarafta yapılmalı
            }
        }


    }
}
