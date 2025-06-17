using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.Varlıklar.Somut
{
    public class IstekLogu : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Yöntem alanı zorunludur.")]
        [StringLength(10, ErrorMessage = "Yöntem en fazla 10 karakter olabilir.")]
        public string Yontem { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yol alanı zorunludur.")]
        [StringLength(200, ErrorMessage = "Yol en fazla 200 karakter olabilir.")]
        public string Yol { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Sorgu parametreleri en fazla 1000 karakter olabilir.")]
        public string SorguParametreleri { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "Başlıklar en fazla 2000 karakter olabilir.")]
        public string Basliklar { get; set; } = string.Empty;

        [StringLength(4000, ErrorMessage = "Gövde en fazla 4000 karakter olabilir.")]
        public string Govde { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kullanıcı bilgisi zorunludur.")]
        public int KullaniciId { get; set; }

        public DateTime IstekZamani { get; set; } = DateTime.Now;
    }

}
