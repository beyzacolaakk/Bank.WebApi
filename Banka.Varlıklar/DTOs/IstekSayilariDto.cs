using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class IstekSayilariDto:IDto     
    {
        public int HesapIstekleri { get; set; }

        public int KartIstekleri { get; set; }

        public int DestekIsdekleri { get; set; }

        public int? LimitArtirmaIstekleri { get; set; }  
    }
}
