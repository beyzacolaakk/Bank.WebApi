using Banka.Cekirdek.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.YardımcıHizmetler.Güvenlik.JWT
{
    public interface ITokenHelper
    {
        AccessToken TokenOlustur(Kullanici kullanici, List<Rol> roller);   
    }
}
