using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class LimitArtirmaServis : ILimitArtirmaServis
    {
        private readonly ILimitArtirmaDal _limitArtirmaDal; 
        private readonly IKartServis _kartServis;

        public LimitArtirmaServis(ILimitArtirmaDal limitArtirmaDal,IKartServis kartServis)
        {
            _limitArtirmaDal = limitArtirmaDal;
            _kartServis = kartServis;
        }

        public async Task<IResult> Ekle(LimitArtirma limitArtirma) 
        {
            await _limitArtirmaDal.Ekle(limitArtirma);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }
        public async Task<IResult> LimitArtirmEkle(LimitArtirmaTalepDto limitArtirma)  
        {
            var data=await _kartServis.IdIleGetir(limitArtirma.KartId);
            var veri = new LimitArtirma{ 
                BasvuruTarihi=DateTime.Now,
                Durum= "Beklemede",
                MevcutLimit= limitArtirma.MevcutLimit,
                KartId= data.Data.Id,
                TalepEdilenLimit= limitArtirma.YeniLimit,
                
            };
            await _limitArtirmaDal.Ekle(veri);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }
        public async Task<IResult> Guncelle(LimitArtirma limitArtirma)
        {
            await _limitArtirmaDal.Guncelle(limitArtirma);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(LimitArtirma limitArtirma)
        {
            await _limitArtirmaDal.Sil(limitArtirma);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }

        public async Task<IDataResult<List<LimitArtirma>>> HepsiniGetir()
        {
            var liste = await _limitArtirmaDal.HepsiniGetir();
            return new SuccessDataResult<List<LimitArtirma>>(liste, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<LimitArtirma>> IdIleGetir(int id)
        {
            var girisOlayi = await _limitArtirmaDal.Getir(x => x.Id == id);
            return new SuccessDataResult<LimitArtirma>(girisOlayi, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<IDataResult<List<LimitArtirmaDto >>> KartLimitIstekleriGetir()  
        {
            var liste = await _limitArtirmaDal.KartLimitIstekleriGetir();
            return new SuccessDataResult<List<LimitArtirmaDto>>(liste, Mesajlar.HepsiniGetirmeBasarili);
        }
        public async Task<IResult> KartLimitIstekGuncelle(LimitArtirmaEkleDto limitArtirmaEkleDto)
        {
            if (limitArtirmaEkleDto.Durum == "Onaylandi")
            {
                decimal mevcutLimit = Convert.ToDecimal(limitArtirmaEkleDto.MevcutLimit);

                decimal yuvarlanmisLimit = Math.Ceiling(mevcutLimit / 5000) * 5000;
                limitArtirmaEkleDto.TalepEdilenLimit = (limitArtirmaEkleDto.TalepEdilenLimit) -(yuvarlanmisLimit-(decimal)limitArtirmaEkleDto.MevcutLimit); 
                var kart = await _kartServis.KartNoIleGetir(limitArtirmaEkleDto.KartNo!);
                var guncelleResult = await _kartServis.KartLimitGuncelle(kart.Data.Id, limitArtirmaEkleDto.TalepEdilenLimit);

                if (guncelleResult.Data)
                {
                    var veri = await _limitArtirmaDal.LimitDurumGuncelle(limitArtirmaEkleDto.Id!.Value, limitArtirmaEkleDto.Durum!);
                    if (veri)
                    {
                        return new SuccessResult(Mesajlar.GuncellemeBasarili);
                    }
                    else
                    {
                        return new ErrorResult(Mesajlar.GuncellemeBasarisiz);
                    }
                }
                else
                {
                    return new ErrorResult("Limit güncellemesi başarısız."); // ← eksik olan yer burasıydı
                }
            }
            else if (limitArtirmaEkleDto.Durum == "Reddedildi")
            {
                var veri = await _limitArtirmaDal.LimitDurumGuncelle(limitArtirmaEkleDto.Id!.Value, limitArtirmaEkleDto.Durum!);
                if (veri)
                {
                    return new SuccessResult(Mesajlar.GuncellemeBasarili);
                }
                else
                {
                    return new ErrorResult(Mesajlar.GuncellemeBasarisiz);
                }
            }
            else
            {
                return new ErrorResult(Mesajlar.GuncellemeBasarisiz);
            }
        }
    


    }
}
