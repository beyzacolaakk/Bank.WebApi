﻿using Banka.Cekirdek.Varlıklar;
using Banka.Cekirdek.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class Kart : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı bilgisi zorunludur.")]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Kart numarası boş olamaz.")]
        [StringLength(16, MinimumLength = 13, ErrorMessage = "Kart numarası 13 ile 16 karakter arasında olmalıdır.")]
        public string KartNumarasi { get; set; }

        [Required(ErrorMessage = "Kart tipi boş olamaz.")]
        [StringLength(20, ErrorMessage = "Kart tipi en fazla 20 karakter olabilir.")]
        public string KartTipi { get; set; }

        [Required(ErrorMessage = "CVV boş olamaz.")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV 3 veya 4 karakter olmalıdır.")]
        public string CVV { get; set; }

        public decimal? Limit { get; set; }
        [Required(ErrorMessage = "Son kullanma tarihi zorunludur.")]
        public DateTime SonKullanma { get; set; }

        public string? Durum { get; set; } 
        public bool Aktif { get; set; } = true;

        public Kullanici? Kullanici { get; set; }

        public ICollection<KartIslem>? KartIslemleri { get; set; }
    }
}
