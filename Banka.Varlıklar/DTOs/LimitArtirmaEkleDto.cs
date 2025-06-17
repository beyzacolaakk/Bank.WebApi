using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class LimitArtirmaEkleDto : IDto
    {
        [Range(0, double.MaxValue, ErrorMessage = "Mevcut limit negatif olamaz.")]
        public decimal? MevcutLimit { get; set; }

        [Required(ErrorMessage = "Talep edilen limit zorunludur.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Talep edilen limit 0'dan büyük olmalıdır.")]
        public decimal TalepEdilenLimit { get; set; }

        [StringLength(16, MinimumLength = 13, ErrorMessage = "Kart numarası 13 ile 16 karakter arasında olmalıdır.")]
        public string? KartNo { get; set; }

        [StringLength(30, ErrorMessage = "Durum en fazla 30 karakter olabilir.")]
        public string? Durum { get; set; }

        public int? Id { get; set; }
    }
}
