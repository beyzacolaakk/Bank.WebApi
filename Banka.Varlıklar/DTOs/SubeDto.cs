using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.DTOs
{
    public class SubeDto:IDto
    {
        public int Id { get; set; }

        public string SubeAdi { get; set; } 
    }
}
