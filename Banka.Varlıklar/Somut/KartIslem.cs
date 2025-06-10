using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class KartIslem : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kart bilgisi zorunludur.")]
        public int KartId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Tutar 0'dan büyük olmalıdır.")]
        public decimal Tutar { get; set; }

        [StringLength(200, ErrorMessage = "Açıklama en fazla 200 karakter olabilir.")]
        public string? Aciklama { get; set; }

        public DateTime IslemTarihi { get; set; } = DateTime.Now;

        public Kart? Kart { get; set; }
    }
}
