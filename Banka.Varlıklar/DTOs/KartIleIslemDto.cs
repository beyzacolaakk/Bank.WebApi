using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KartIleIslemDto:IDto
    {
        public int KartId { get; set; }
        public decimal Tutar {  get; set; }

        public int KullaniciId { get; set; }
    }
}
