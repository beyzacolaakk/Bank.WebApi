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
    public class Kayit : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı bilgisi zorunludur.")]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Eylem alanı boş olamaz.")]
        [StringLength(100, ErrorMessage = "Eylem en fazla 100 karakter olabilir.")]
        public string Eylem { get; set; }

        [StringLength(500, ErrorMessage = "Detay en fazla 500 karakter olabilir.")]
        public string Detay { get; set; }

        [Required(ErrorMessage = "Tür alanı boş olamaz.")]
        [RegularExpression("bilgi|uyari|hata", ErrorMessage = "Tür sadece 'bilgi', 'uyari' veya 'hata' olabilir.")]
        public string Tur { get; set; } 

        public DateTime Tarih { get; set; } = DateTime.Now;

        public Kullanici? Kullanici { get; set; }
    }
}
