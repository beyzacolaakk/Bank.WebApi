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
    public class GirisToken : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı bilgisi zorunludur.")]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Token boş olamaz.")]
        [StringLength(500, ErrorMessage = "Token en fazla 500 karakter olabilir.")]
        public string Token { get; set; } = string.Empty;

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Geçerlilik bitiş tarihi zorunludur.")]
        public DateTime GecerlilikBitis { get; set; }

        public Kullanici? Kullanici { get; set; }
    }

}
