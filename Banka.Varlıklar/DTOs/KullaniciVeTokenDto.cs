﻿using Banka.Cekirdek.Varlıklar;
using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Güvenlik.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class KullaniciVeTokenDto:IDto
    {
        public AccessToken Token { get; set; }
    }
}
