using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class DestekTalebi
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Konu { get; set; }
        public string Mesaj { get; set; }
        public string Durum { get; set; } 
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        public Kullanici Kullanici { get; set; }
    }
}
