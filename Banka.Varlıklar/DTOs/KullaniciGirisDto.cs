using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KullaniciGirisDto:IDto
    {
        [Required(ErrorMessage = "Telefon boş olamaz.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(int.MaxValue, MinimumLength = 11, ErrorMessage = "Telefon numarası en az 11 haneli olmalıdır.")]
        public string Telefon { get; set; } 

        [Required(ErrorMessage = "Şifre boş olamaz.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 en fazla 100 karakter olmalıdır.")]
        public string Sifre { get; set; }

        public string IpAdres { get; set; } 
    }
}
