using Banka.Cekirdek.Varlıklar;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class VarlıklarDto:IDto
    {
        public decimal? ToplamPara { get; set; }

        public decimal? ToplamBorc { get; set; } 

        public List<HesapDto> Hesaplar { get; set; }

        public List<KartDto> Kartlar { get; set; } 
    }
}
