using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class LimitArtirma:IEntity 
    {
        public int Id { get; set; }

        public decimal MevcutLimit { get; set; }

        public decimal TalepEdilenLimit { get; set; }

        public DateTime BaşvuruTarihi { get; set; }

        public string Durum { get; set; }

        public int KartId { get; set; } 
    }
}
