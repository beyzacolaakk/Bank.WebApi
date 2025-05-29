using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.Varlıklar.Somut
{
    public class Kullanici:IEntity
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public byte[] SifreHash { get; set; }
        public byte[] SifreSalt { get; set; }
        public bool Aktif { get; set; } = true;

        public int SubeId { get; set; }


        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}
