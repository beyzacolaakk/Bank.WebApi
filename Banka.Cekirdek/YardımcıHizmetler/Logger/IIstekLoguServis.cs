using Banka.Cekirdek.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.YardımcıHizmetler.Logger
{
    public interface IIstekLoguServis
    {
        Task LogIstekAsync(IstekLogu log);
    }
}
