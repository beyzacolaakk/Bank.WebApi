using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
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

        public LimitArtirmaServis(ILimitArtirmaDal limitArtirmaDal)
        {
            _limitArtirmaDal = limitArtirmaDal;
        }

        public async Task<IResult> Ekle(LimitArtirma limitArtirma) 
        {
            await _limitArtirmaDal.Ekle(limitArtirma);
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
    }
}
