using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class KullaniciRol
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}
