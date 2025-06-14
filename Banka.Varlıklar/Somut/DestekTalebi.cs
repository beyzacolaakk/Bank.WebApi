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
        public string Konu { get; set; }

        [Required(ErrorMessage = "Mesaj alanı boş bırakılamaz.")]
        [StringLength(1000, ErrorMessage = "Mesaj en fazla 1000 karakter olabilir.")]
        public string Mesaj { get; set; }

        [Required(ErrorMessage = "Durum bilgisi zorunludur.")]
        [StringLength(30, ErrorMessage = "Durum en fazla 30 karakter olabilir.")]
        public string Durum { get; set; }

        public string? Yanit { get; set; } 

        public string? Kategori {  get; set; }
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        public Kullanici? Kullanici { get; set; }
    }
}
