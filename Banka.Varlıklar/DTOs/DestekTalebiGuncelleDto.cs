using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class DestekTalebiGuncelleDto:IDto
    {
        [Required(ErrorMessage = "Id zorunludur.")]
        public int Id { get; set; }  // Güncelleme için Id zorunlu olmalı, nullable olmamalı

        [StringLength(30, ErrorMessage = "Durum en fazla 30 karakter olabilir.")]
        public string? Durum { get; set; }

        [StringLength(2000, ErrorMessage = "Yanıt en fazla 2000 karakter olabilir.")]
        public string? Yanit { get; set; }
    }

}
