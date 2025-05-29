using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KullaniciKayitDto:IDto
    {
        public string Telefon { get; set; }
        public string AdSoyad { get; set; } 
        public string Email { get; set; } 
        public string Sifre { get; set; }
        public int Sube { get; set; }  
        public string Aktif { get; set; }
    }
}
