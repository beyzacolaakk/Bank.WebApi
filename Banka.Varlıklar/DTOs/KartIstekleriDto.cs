using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KartIstekleriDto:IDto
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }

        public string KartTipi { get; set; }

        public decimal? Limit { get; set; }

        public DateTime Tarih { get; set; }

        public string Durum { get; set; }


    }
}
