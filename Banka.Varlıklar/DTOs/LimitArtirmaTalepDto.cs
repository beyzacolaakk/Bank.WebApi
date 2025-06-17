using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class LimitArtirmaTalepDto : IDto
    {
        [Required(ErrorMessage = "Kart Id zorunludur.")]
        public int KartId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Mevcut limit negatif olamaz.")]
        public decimal MevcutLimit { get; set; }

        [Required(ErrorMessage = "Yeni limit zorunludur.")]
        [Range(1, double.MaxValue, ErrorMessage = "Yeni limit en az 1 olmalıdır.")]
        public decimal YeniLimit { get; set; }
    }
}
