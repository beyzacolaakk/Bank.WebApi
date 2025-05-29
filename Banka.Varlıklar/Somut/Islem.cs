using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class Islem
    {
        public int Id { get; set; }
        public int? GonderenHesapId { get; set; }
        public int? AliciHesapId { get; set; }
        public decimal Tutar { get; set; }
        public string IslemTipi { get; set; }
        public string Durum { get; set; }
        public string Aciklama { get; set; }
        public DateTime IslemTarihi { get; set; } = DateTime.Now;


        public Hesap GonderenHesap { get; set; }
        public Hesap AliciHesap { get; set; }
    }
}
