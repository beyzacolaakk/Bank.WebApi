using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KullaniciBilgileriDto:IDto
    {

        public string AdSoyad { get; set; }
        public string Email { get; set; } 

        public string Telefon {  get; set; }  

        public string Sube { get; set; } 
    }
}
