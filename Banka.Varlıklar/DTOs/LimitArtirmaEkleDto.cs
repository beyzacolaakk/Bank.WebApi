using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class LimitArtirmaEkleDto:IDto
    {

        public decimal? MevcutLimit { get; set; }

        public decimal TalepEdilenLimit { get; set; }

        public string? KartNo { get; set; }

        public string? Durum { get; set; }

        public int? Id { get; set; } 
    }
}
