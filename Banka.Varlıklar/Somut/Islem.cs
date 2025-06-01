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

        [Range(0.01, double.MaxValue, ErrorMessage = "Tutar 0'dan büyük olmalıdır.")]
        public decimal Tutar { get; set; }

        [Required(ErrorMessage = "İşlem tipi boş olamaz.")]
        [StringLength(50, ErrorMessage = "İşlem tipi en fazla 50 karakter olabilir.")]
        public string IslemTipi { get; set; }

        [Required(ErrorMessage = "Durum boş olamaz.")]
        [StringLength(20, ErrorMessage = "Durum en fazla 20 karakter olabilir.")]
        public string Durum { get; set; }

        [StringLength(200, ErrorMessage = "Açıklama en fazla 200 karakter olabilir.")]
        public string Aciklama { get; set; }

        public DateTime IslemTarihi { get; set; } = DateTime.Now;

        public Hesap? GonderenHesap { get; set; }

        public Hesap? AliciHesap { get; set; }
    }
}
