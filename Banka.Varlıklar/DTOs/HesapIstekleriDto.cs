using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class HesapIstekleriDto : IDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad boş olamaz.")]
        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
        public string AdSoyad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hesap numarası boş olamaz.")]
        [StringLength(20, ErrorMessage = "Hesap numarası en fazla 20 karakter olabilir.")]
        public string HesapNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Başvuru tarihi zorunludur.")]
        public DateTime BasvuruTarihi { get; set; }

        [Required(ErrorMessage = "Durum boş olamaz.")]
        [StringLength(30, ErrorMessage = "Durum en fazla 30 karakter olabilir.")]
        public string Durum { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon boş olamaz.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Telefon numarası 10 ile 15 karakter arasında olmalıdır.")]
        public string Telefon { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi boş olamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Eposta { get; set; } = string.Empty;
    }
}
