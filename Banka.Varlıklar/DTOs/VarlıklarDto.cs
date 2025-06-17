using Banka.Cekirdek.Varlıklar;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class VarlıklarDto : IDto 
    {
        [Range(0, double.MaxValue, ErrorMessage = "Toplam para negatif olamaz.")]
        public decimal? ToplamPara { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Toplam borç negatif olamaz.")]
        public decimal? ToplamBorc { get; set; }

        [Required(ErrorMessage = "Hesaplar listesi boş olamaz.")]
        public List<HesapDto> Hesaplar { get; set; } = new();

        [Required(ErrorMessage = "Kartlar listesi boş olamaz.")]
        public List<KartDto> Kartlar { get; set; } = new();
    }
}
