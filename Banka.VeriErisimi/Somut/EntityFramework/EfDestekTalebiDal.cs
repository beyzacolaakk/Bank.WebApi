using Banka.Cekirdek.VeriErisimi.EntityFramework;
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
    }
}
