using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.VeriErisimi.EntityFramework;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisim.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.VeriErisimi.Somut.EntityFramework
{
    public class EfKullaniciDal : EfEntityRepositoryBase<Kullanici, BankaContext>, IKullaniciDal
    {
        public async Task<List<Rol>> YetkileriGetir(Kullanici kullanici)
        {
            using (var context = new BankaContext())
            {
                var result = from rol in context.Roller
                             join kullaniciRol in context.KullaniciRolleri
                                 on rol.Id equals kullaniciRol.RolId  
                             where kullaniciRol.KullaniciId == kullanici.Id  
                             select new Rol { Id = rol.Id, RolAdi = rol.RolAdi };

                return await result.ToListAsync();
            }
        }

        public async Task<Kullanici> EkleVeIdGetirAsync(Kullanici kullanici)
        {
            using (var context = new BankaContext())
            {
                await context.Kullanicilar.AddAsync(kullanici);
                await context.SaveChangesAsync();
                return kullanici;
            }
        }

        public async Task<string> IdIleKullaniciAdiGetirAsync(int id)
        {
            using (var context = new BankaContext())
            {
                return await context.Kullanicilar
                                    .Where(k => k.Id == id)
                                    .Select(k => k.Telefon)
                                    .FirstOrDefaultAsync();
            }
        }
        public async Task<KullaniciBilgileriDto> KullaniciGetir(int id) 
        {
            using (var context = new BankaContext())
            {
                return await (from k in context.Kullanicilar
                              join s in context.Subeler on k.SubeId equals s.Id
                              where k.Id == id
                              select new KullaniciBilgileriDto
                              {
                                  AdSoyad = k.AdSoyad,
                                  Email = k.Email,
                                  Telefon = k.Telefon,
                                  Sube = s.SubeAdi 
                              }).FirstOrDefaultAsync();
            }
        }



    }
}
