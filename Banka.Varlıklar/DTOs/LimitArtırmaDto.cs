using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class LimitArtirmaDto:IDto 
    {
        public int Id { get; set; }

        public int KullaniciId { get; set; } 
        public string AdSoyad { get; set; }

        public string KartNo { get; set; } 

        public decimal? MevcutLimit { get; set; }

        public decimal? TalepEdilenLimit { get; set; }

        public DateTime BasvuruTarihi { get; set; }

        public string Durum { get; set; }   
    }
}
