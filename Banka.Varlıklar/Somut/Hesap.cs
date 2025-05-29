using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class Hesap
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string HesapNo { get; set; }
        public string HesapTipi { get; set; }
        public decimal Bakiye { get; set; }
        public string ParaBirimi { get; set; } = "TRY";
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;


        public Kullanici Kullanici { get; set; }
        public ICollection<Islem> GonderenIslemler { get; set; }
        public ICollection<Islem> AliciIslemler { get; set; }
    }
}
