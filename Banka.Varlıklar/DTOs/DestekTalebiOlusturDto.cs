using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class DestekTalebiOlusturDto : IDto
    {
        public int Id { get; set; }  // Oluşturma sırasında genelde Id verilmez, backend atar, istersen kaldırabilirsin.

        [Required(ErrorMessage = "Kullanıcı bilgisi zorunludur.")]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Konu alanı boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Konu en fazla 100 karakter olabilir.")]
        public string Konu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mesaj alanı boş bırakılamaz.")]
        [StringLength(2000, ErrorMessage = "Mesaj en fazla 2000 karakter olabilir.")]
        public string Mesaj { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Kategori en fazla 50 karakter olabilir.")]
        public string? Kategori { get; set; }

        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
        public string? AdSoyad { get; set; }

        public DateTime? Tarih { get; set; }  // Oluşturma tarihi genelde backend tarafından atanır.

        [StringLength(2000, ErrorMessage = "Yanıt en fazla 2000 karakter olabilir.")]
        public string? Yanit { get; set; }

        [StringLength(30, ErrorMessage = "Durum en fazla 30 karakter olabilir.")]
        public string? Durum { get; set; }
    }

}
