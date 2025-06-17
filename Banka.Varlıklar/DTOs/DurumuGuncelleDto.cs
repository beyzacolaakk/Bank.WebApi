using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class DurumuGuncelleDto : IDto
    {
        [Required(ErrorMessage = "Id zorunludur.")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Durum alanı boş bırakılamaz.")]
        [StringLength(30, ErrorMessage = "Durum en fazla 30 karakter olabilir.")]
        public string? Durum { get; set; }
    }

}
