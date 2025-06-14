﻿using Banka.Cekirdek.VeriErisimi;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.VeriErisimi.Soyut
{
    public interface IIslemDal : IEntityRepository<Islem>
    {
        List<Islem> GetirIslemleri(List<int> hesapIdler);
    }
}
