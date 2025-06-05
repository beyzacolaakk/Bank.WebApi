using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KartDto : IDto
    {
        public string KartNumarasi { get; set; }
        public decimal? Limit { get; set; } 
    }
}
