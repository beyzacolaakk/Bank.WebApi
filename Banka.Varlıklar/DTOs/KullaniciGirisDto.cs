using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KullaniciGirisDto:IDto
    {
        public string Telefon { get; set; } 
        public string Sifre { get; set; } 
    }
}
