using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class SonHareketlerDto:IDto
    {
        public decimal GuncelBakiye { get; set; }
        public decimal Tutar { get; set; }

        public string IslemTipi { get; set; }

        public DateTime Tarih {  get; set; }

        public string? Aciklama {  get; set; } 

        public string Durum {  get; set; }
    }
}
