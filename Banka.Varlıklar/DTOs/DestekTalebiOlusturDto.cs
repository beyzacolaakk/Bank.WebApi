using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class DestekTalebiOlusturDto:IDto
    {
        public int KullaniciId { get; set; }

        public string Konu { get; set; }

        public string Mesaj { get; set; }

        public string Kategori { get; set; }
    }
}
