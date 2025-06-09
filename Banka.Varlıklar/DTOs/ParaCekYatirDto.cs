using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class ParaCekYatirDto
    {
        public int KullaniciId { get; set; }

        public decimal Tutar { get; set; }

        public string IslemTipi { get; set; }

        public string Aciklama { get; set; }

        public string HesapId {  get; set; }

        public string IslemTuru {  get; set; }

    }
}
