using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class GirisOlayi
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string IpAdresi { get; set; }
        public bool Basarili { get; set; }
        public DateTime Zaman { get; set; } = DateTime.Now;
        public Kullanici Kullanici { get; set; }
    }

}
