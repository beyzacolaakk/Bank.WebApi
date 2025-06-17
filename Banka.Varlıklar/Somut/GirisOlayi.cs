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
    public class GirisOlayi : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı bilgisi zorunludur.")]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "IP adresi boş olamaz.")]
        [StringLength(45, ErrorMessage = "IP adresi en fazla 45 karakter olabilir.")]
        public string IpAdresi { get; set; } = string.Empty;

        public bool Basarili { get; set; }

        public DateTime Zaman { get; set; } = DateTime.Now;

        public Kullanici? Kullanici { get; set; }
    }

}
