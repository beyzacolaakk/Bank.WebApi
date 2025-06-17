using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.Varlıklar.Somut
{
    public class Rol:IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rol adı boş olamaz.")]
        [StringLength(50, ErrorMessage = "Rol adı en fazla 50 karakter olabilir.")]
        public string RolAdi { get; set; }
    }
}
