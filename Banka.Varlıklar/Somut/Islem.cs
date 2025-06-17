using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class Islem : IEntity
    {
        public int Id { get; set; }

        public int? GonderenHesapId { get; set; }

        public int? AliciHesapId { get; set; }

        public int? KartId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Tutar 0'dan büyük olmalıdır.")]
        public decimal Tutar { get; set; }

        public decimal? GuncelBakiye { get; set; }

        [Required(ErrorMessage = "İşlem tipi boş olamaz.")]
        [StringLength(50, ErrorMessage = "İşlem tipi en fazla 50 karakter olabilir.")]
        public string IslemTipi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Durum boş olamaz.")]
        [StringLength(20, ErrorMessage = "Durum en fazla 20 karakter olabilir.")]
        public string Durum { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string? Aciklama { get; set; }

        public DateTime IslemTarihi { get; set; } = DateTime.Now;

        public Hesap? GonderenHesap { get; set; }

        public Hesap? AliciHesap { get; set; }
    }

}
