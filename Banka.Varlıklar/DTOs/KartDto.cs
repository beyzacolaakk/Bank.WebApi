using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KartDto : IDto
    {
        [Required(ErrorMessage = "Kart numarası boş olamaz.")]
        [StringLength(16, MinimumLength = 13, ErrorMessage = "Kart numarası 13 ile 16 karakter arasında olmalıdır.")]
        public string KartNumarasi { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Limit negatif olamaz.")]
        public decimal? Limit { get; set; }
    }
}
