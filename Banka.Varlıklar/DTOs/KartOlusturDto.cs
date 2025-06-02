using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KartOlusturDto:IDto
    {
        public int KullaniciId { get; set; }  
        public string KartTipi { get; set; } 
    }
}
