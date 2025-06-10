using Banka.Cekirdek.VeriErisimi.EntityFramework;
using Banka.Varlıklar.Somut;
using Banka.VeriErisim.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.VeriErisimi.Somut.EntityFramework
{
    public class EfIslemDal : EfEntityRepositoryBase<Islem, BankaContext>, IIslemDal
    {
        public List<Islem> GetirIslemleri(List<int> hesapIdler)
        {
            using (var context = new BankaContext())
            {
                return context.Islemler
                              .Where(ki => ki.GonderenHesapId != null && hesapIdler.Contains(ki.GonderenHesapId.Value))
                              .ToList();
            }
        }
    }
}
