using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.Varlıklar.Somut
{
    public class IstekLogu : IEntity
    {
        public int Id { get; set; }
        public string Yontem { get; set; }
        public string Yol { get; set; }
        public string SorguParametreleri { get; set; }
        public string Basliklar { get; set; }
        public string Govde { get; set; }
        public int KullaniciId { get; set; } 
        public DateTime IstekZamani { get; set; } = DateTime.Now;
    }
}
