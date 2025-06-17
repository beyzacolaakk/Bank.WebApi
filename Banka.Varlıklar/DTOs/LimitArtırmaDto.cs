using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class LimitArtirmaDto : IDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı Id zorunludur.")]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Ad Soyad boş olamaz.")]
        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
        public string AdSoyad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kart numarası boş olamaz.")]
        [StringLength(16, MinimumLength = 13, ErrorMessage = "Kart numarası 13 ile 16 karakter arasında olmalıdır.")]
        public string KartNo { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Mevcut limit negatif olamaz.")]
        public decimal? MevcutLimit { get; set; }

        [Required(ErrorMessage = "Talep edilen limit zorunludur.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Talep edilen limit 0'dan büyük olmalıdır.")]
        public decimal? TalepEdilenLimit { get; set; }

        public DateTime BasvuruTarihi { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Durum zorunludur.")]
        [StringLength(30, ErrorMessage = "Durum en fazla 30 karakter olabilir.")]
        public string Durum { get; set; } = string.Empty;
    }
}
