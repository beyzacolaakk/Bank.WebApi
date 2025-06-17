using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.Varlıklar.Somut
{
    public class Kullanici : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad boş olamaz.")]
        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
        public string AdSoyad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email boş olamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        [StringLength(100, ErrorMessage = "Email en fazla 100 karakter olabilir.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon boş olamaz.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Telefon numarası 10 ile 15 karakter arasında olmalıdır.")]
        public string Telefon { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre hash zorunludur.")]
        public byte[] SifreHash { get; set; } = Array.Empty<byte>();

        [Required(ErrorMessage = "Şifre salt zorunludur.")]
        public byte[] SifreSalt { get; set; } = Array.Empty<byte>();

        public bool Aktif { get; set; } = true;

        [Required(ErrorMessage = "Şube Id zorunludur.")]
        public int SubeId { get; set; }

        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }

}
