using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.VeriErisimi.EntityFramework;
using Banka.Varlıklar.Somut;
using Banka.VeriErisim.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
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
        public List<Rol> YetkileriGetir(Kullanici kullanici)
        {
            using (var context = new BankaContext())
            { 
                var result = from rol in context.Roller
                             join KullaniciRol in context.KullaniciRolleri 
                                 on rol.Id equals KullaniciRol.Id
                             where KullaniciRol.Id == kullanici.Id
                             select new Rol { Id = rol.Id, RolAdi = rol.RolAdi };
                return result.ToList();

            }
        }
        public Kullanici EkleVeIdGetir(Kullanici kullanici)
        {
            using (BankaContext context = new BankaContext())
            {
                context.Kullanicilar.Add(kullanici);
                context.SaveChanges();
                return kullanici;
            }
        }


        public string IdIleKullaniciAdiGetir(int id)
        {
            using (BankaContext context = new BankaContext())
            {
                return context.Kullanicilar
                              .Where(k => k.Id == id)
                              .Select(k => k.Telefon)
                              .FirstOrDefault();
            }
        }





    }
}
