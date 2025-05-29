using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class Kayit
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Eylem { get; set; }
        public string Detay { get; set; }
        public string Tur { get; set; } // "bilgi", "uyari", "hata"
        public DateTime Tarih { get; set; } = DateTime.Now;

        public Kullanici Kullanici { get; set; }
    }
}
