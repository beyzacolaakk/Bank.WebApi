using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class LimitArtirmaTalepDto:IDto
    {

        public int KartId { get; set; }

        public decimal MevcutLimit { get; set; } 

        public int YeniLimit { get; set; } 
    }
}
