using Banka.Cekirdek.Varlıklar;
using Banka.Cekirdek.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class DestekTalebi : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı bilgisi zorunludur.")]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Konu alanı boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Konu en fazla 100 karakter olabilir.")]
        public string Konu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mesaj alanı boş bırakılamaz.")]
        [StringLength(2000, ErrorMessage = "Mesaj en fazla 2000 karakter olabilir.")]
        public string Mesaj { get; set; } = string.Empty;

        [Required(ErrorMessage = "Durum bilgisi zorunludur.")]
        [StringLength(30, ErrorMessage = "Durum en fazla 30 karakter olabilir.")]
        public string Durum { get; set; } = "Beklemede";

        [StringLength(2000, ErrorMessage = "Yanıt en fazla 2000 karakter olabilir.")] 
        public string? Yanit { get; set; }

        [StringLength(50, ErrorMessage = "Kategori en fazla 50 karakter olabilir.")]
        public string? Kategori { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        public Kullanici? Kullanici { get; set; }
    }

}
