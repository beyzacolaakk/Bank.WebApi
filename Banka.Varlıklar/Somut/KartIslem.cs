using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class KartIslem : IEntity
    {
        public int Id { get; set; }
        public int KartId { get; set; }
        public decimal Tutar { get; set; }
        public string Aciklama { get; set; }
        public DateTime IslemTarihi { get; set; } = DateTime.Now;

        public Kart Kart { get; set; }
    }
}
