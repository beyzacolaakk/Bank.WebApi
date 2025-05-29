using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class Sube
    {
        public int Id { get; set; }
        public string SubeAdi { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public ICollection<Kullanici> Kullanicilar { get; set; } // One-to-many
    }

}
