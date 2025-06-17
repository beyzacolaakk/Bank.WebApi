using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.Varlıklar.Somut
{
    public class KullaniciRol:IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı Id zorunludur.")]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Rol Id zorunludur.")]
        public int RolId { get; set; }
    }
}
