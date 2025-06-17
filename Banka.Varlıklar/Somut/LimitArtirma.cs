using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class LimitArtirma : IEntity
    {
        public int Id { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Mevcut limit negatif olamaz.")]
        public decimal MevcutLimit { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Talep edilen limit 0'dan büyük olmalıdır.")]
        public decimal TalepEdilenLimit { get; set; }

        [Required(ErrorMessage = "Başvuru tarihi zorunludur.")]
        public DateTime BasvuruTarihi { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Durum boş olamaz.")]
        [StringLength(30, ErrorMessage = "Durum en fazla 30 karakter olabilir.")]
        public string Durum { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kart bilgisi zorunludur.")]
        public int KartId { get; set; }
    }

}
