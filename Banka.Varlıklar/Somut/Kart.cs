using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class Kart
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string KartNumarasi { get; set; }
        public string KartTipi { get; set; }
        public string CVV { get; set; }
        public DateTime SonKullanma { get; set; }
        public bool Aktif { get; set; } = true;


        public Kullanici Kullanici { get; set; }
        public ICollection<KartIslem> KartIslemleri { get; set; }
    }
}
