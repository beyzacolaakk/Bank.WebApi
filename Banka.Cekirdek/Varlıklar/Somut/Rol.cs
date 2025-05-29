using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.Varlıklar.Somut
{
    public class Rol:IEntity
    {
        public int Id { get; set; }
        public string RolAdi { get; set; } 
    }
}
