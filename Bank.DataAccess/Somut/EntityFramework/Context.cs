using Banka.Varlıklar.Somut;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.VeriErisim.Somut.EntityFramework
{
    public class BankaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-DNUIALQ\SQLKOD;Database=BankaDb;integrated Security=true;TrustServerCertificate=True;");
        }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Hesap> Hesaplar { get; set; }
        public DbSet<Islem> Islemler { get; set; }
        public DbSet<Kart> Kartlar { get; set; }
        public DbSet<KartIslem> KartIslemleri { get; set; }
        public DbSet<GirisOlayi> GirisOlaylari { get; set; }
        public DbSet<GirisToken> GirisTokenlari { get; set; }
        public DbSet<Sube> Subeler { get; set; }
        public DbSet<DestekTalebi> DestekTalepleri { get; set; }
        public DbSet<Kayıtlar> Kayıtlar { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Islem>()
                .HasOne(i => i.GonderenHesap)
                .WithMany(h => h.GonderenIslemler)
                .HasForeignKey(i => i.GonderenHesapId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Islem>()
                .HasOne(i => i.AliciHesap)
                .WithMany(h => h.AliciIslemler)
                .HasForeignKey(i => i.AliciHesapId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
