﻿using Banka.Cekirdek.Varlıklar.Somut;
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
    public class EfKartIslemDal : EfEntityRepositoryBase<KartIslem ,BankaContext>, IKartIslemDal
    {
        public EfKartIslemDal(BankaContext context) 
        {
        }
        public List<KartIslem> GetirKartIslemleri(List<int> kartIdler)
        {
            using (var context = new BankaContext())
            {
                return context.KartIslemleri
                              .Where(ki => kartIdler.Contains(ki.KartId))
                              .ToList(); 
            }
        }
    }
}
