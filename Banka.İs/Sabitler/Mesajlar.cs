using Banka.Cekirdek.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Sabitler
{
    public static class Mesajlar 
    {

        public static string KullaniciEklemeBasarili = "Kullanici Eklendi"; 
         
        public static string KullaniciEklemeBasarisiz = "Kullanici Eklendi";

        public static string KullaniciGuncellemeBasarili { get; internal set; }
        public static string KullaniciSilmeBasarili { get; internal set; }
        public static Kullanici KullanıcıBulunamadı { get; internal set; }
        public static Kullanici HatalıGiris { get; internal set; }
        public static string GirisBasarili { get; internal set; }
    }
}
