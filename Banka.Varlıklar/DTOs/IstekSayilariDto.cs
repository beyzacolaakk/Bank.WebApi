using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class IstekSayilariDto : IDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Hesap istekleri negatif olamaz.")]
        public int HesapIstekleri { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Kart istekleri negatif olamaz.")]
        public int KartIstekleri { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Destek istekleri negatif olamaz.")]
        public int DestekIstekleri { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Limit artırma istekleri negatif olamaz.")]
        public int? LimitArtirmaIstekleri { get; set; }
    }
}
