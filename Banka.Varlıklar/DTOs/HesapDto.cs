using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Banka.Varlıklar.DTOs
{


    public class HesapDto : IDto
    {
        [Required(ErrorMessage = "Hesap tipi boş olamaz.")]
        [StringLength(30, ErrorMessage = "Hesap tipi en fazla 30 karakter olabilir.")]
        public string HesapTipi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Para birimi boş olamaz.")]
        [StringLength(5, ErrorMessage = "Para birimi en fazla 5 karakter olabilir.")]
        public string ParaBirimi { get; set; } = "TL";

        [Range(0, double.MaxValue, ErrorMessage = "Bakiye negatif olamaz.")]
        public decimal Bakiye { get; set; }
    }

}
