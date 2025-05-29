using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Varlıklar.Somut
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string SifreHash { get; set; }
        public string SifreSalt { get; set; }
        public bool Aktif { get; set; } = true;

        public int SubeId { get; set; }  // Foreign Key
        public Sube Sube { get; set; }   // Navigation Property

        public DateTime KayitTarihi { get; set; } = DateTime.Now;

        public ICollection<Hesap> Hesaplar { get; set; }
        public ICollection<Kart> Kartlar { get; set; }
        public ICollection<GirisOlayi> GirisOlaylari { get; set; }
        public ICollection<GirisToken> GirisTokenlari { get; set; }
        public ICollection<DestekTalebi> DestekTalepleri { get; set; }
        public ICollection<Kayit> Kayitlar { get; set; }
        public ICollection<KullaniciRol> KullaniciRolleri { get; set; }
    }
}
