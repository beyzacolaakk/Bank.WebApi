using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KartOlusturDto : IDto
    {
        [Required(ErrorMessage = "Kullanıcı bilgisi zorunludur.")]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Kart tipi boş olamaz.")]
        [StringLength(20, ErrorMessage = "Kart tipi en fazla 20 karakter olabilir.")]
        public string KartTipi { get; set; }
    }
}
