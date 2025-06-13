using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.VeriErisimi.EntityFramework;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisim.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.VeriErisimi.Somut.EntityFramework
{
    public class EfLimitArtirmaDal : EfEntityRepositoryBase<LimitArtirma, BankaContext>, ILimitArtirmaDal
    {
        public async Task<List<LimitArtirmaDto>> KartLimitIstekleriGetir()
        {
            using (var context = new BankaContext())
            {
                var result = await (from limitArtirma in context.LimitArtirma
                                    join kart in context.Kartlar on limitArtirma.KartId equals kart.Id
                                    join kullanici in context.Kullanicilar on kart.KullaniciId equals kullanici.Id
                                    select new LimitArtirmaDto 
                                    {
                                        Id=limitArtirma.Id, 
                                        KullaniciId=kullanici.Id,
                                        AdSoyad = kullanici.AdSoyad,
                                        Durum = limitArtirma.Durum,
                                        BasvuruTarihi = limitArtirma.BaşvuruTarihi, // doğru bilgi buradan alınır
                                        KartNo = kart.KartNumarasi,
                                        MevcutLimit = kart.Limit ?? 0,
                                        TalepEdilenLimit = limitArtirma.TalepEdilenLimit,

                                    }).ToListAsync();

                return result;
            }
        }

    }
}
