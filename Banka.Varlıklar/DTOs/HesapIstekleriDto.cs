using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class HesapIstekleriDto:IDto
    {
        public int Id { get; set; }

        public string AdSoyad { get; set; }

        public string HesapNo { get; set; }

        public DateTime BasvuruTarihi { get; set; } 

        public string Durum { get; set; }

        public string Telefon { get; set; }

        public string Eposta { get; set; } 
    }
}
