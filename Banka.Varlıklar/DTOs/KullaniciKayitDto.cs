using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KullaniciKayitDto : IDto
    {
        [Required(ErrorMessage = "Ad Soyad boş olamaz.")]
        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
        public string AdSoyad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email boş olamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon boş olamaz.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Telefon numarası 10 ile 15 karakter arasında olmalıdır.")]
        public string Telefon { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre boş olamaz.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Sifre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şube boş olamaz.")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir şube seçiniz.")]
        public int Sube { get; set; }

        public bool Aktif { get; set; } = true;
    }
}
