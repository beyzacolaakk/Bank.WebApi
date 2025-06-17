using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class ParaCekYatirDto
    {
        [Required(ErrorMessage = "Kullanıcı Id zorunludur.")]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Tutar zorunludur.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Tutar 0'dan büyük olmalıdır.")]
        public decimal Tutar { get; set; }

        [Required(ErrorMessage = "İşlem tipi zorunludur.")]
        [StringLength(50, ErrorMessage = "İşlem tipi en fazla 50 karakter olabilir.")]
        public string IslemTipi { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Açıklama en fazla 200 karakter olabilir.")]
        public string? Aciklama { get; set; }

        [Required(ErrorMessage = "Hesap Id zorunludur.")]
        public string HesapId { get; set; } = string.Empty;

        [Required(ErrorMessage = "İşlem türü zorunludur.")]
        [StringLength(50, ErrorMessage = "İşlem türü en fazla 50 karakter olabilir.")]
        public string IslemTuru { get; set; } = string.Empty;
    }
}
