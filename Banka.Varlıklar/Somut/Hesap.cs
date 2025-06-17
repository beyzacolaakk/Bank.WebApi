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
    public class Hesap : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı bilgisi zorunludur.")]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Hesap numarası boş olamaz.")]
        [StringLength(20, ErrorMessage = "Hesap numarası en fazla 20 karakter olabilir.")]
        public string HesapNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hesap tipi boş olamaz.")]
        [StringLength(30, ErrorMessage = "Hesap tipi en fazla 30 karakter olabilir.")]
        public string HesapTipi { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Bakiye negatif olamaz.")]
        public decimal Bakiye { get; set; }

        [Required(ErrorMessage = "Para birimi boş olamaz.")]
        [StringLength(5, ErrorMessage = "Para birimi en fazla 5 karakter olabilir.")]
        public string ParaBirimi { get; set; } = "TL";

        [StringLength(30, ErrorMessage = "Durum en fazla 30 karakter olabilir.")]
        public string? Durum { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        public Kullanici? Kullanici { get; set; }

        public ICollection<Islem>? GonderenIslemler { get; set; }

        public ICollection<Islem>? AliciIslemler { get; set; }
    }

}
