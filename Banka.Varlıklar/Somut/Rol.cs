using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class Rol
    {
        public int Id { get; set; }
        public string RoleAdi { get; set; }
        public ICollection<KullaniciRol> KullaniciRolleri { get; set; }
    }
}
