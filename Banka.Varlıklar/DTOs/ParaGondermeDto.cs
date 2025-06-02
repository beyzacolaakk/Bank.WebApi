using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class ParaGondermeDto:IDto
    {
        public int KullaniciId { get; set; } 
        public int GonderenHesapId { get; set; }

        public int AliciHesapId { get; set; }

        public decimal Tutar { get; set; }

        public string IslemTipi { get; set; }

        public string Aciklama { get; set; }

    }
}
