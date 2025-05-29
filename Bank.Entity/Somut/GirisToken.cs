using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class GirisToken
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Token { get; set; }
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        public DateTime GecerlilikBitis { get; set; }

        public Kullanici Kullanici { get; set; }
    }

}
