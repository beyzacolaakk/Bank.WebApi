using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KartIstekleriDto : IDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad boş olamaz.")]
        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
        public string AdSoyad { get; set; }

        [Required(ErrorMessage = "Kart tipi boş olamaz.")]
        [StringLength(20, ErrorMessage = "Kart tipi en fazla 20 karakter olabilir.")]
        public string KartTipi { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Limit negatif olamaz.")]
        public decimal? Limit { get; set; }

        public DateTime Tarih { get; set; }

        [Required(ErrorMessage = "Durum bilgisi boş olamaz.")]
        [StringLength(30, ErrorMessage = "Durum en fazla 30 karakter olabilir.")]
        public string Durum { get; set; }
    }
}
