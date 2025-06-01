using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Soyut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class GirisOlayiServis : IGirisOlayiServis
    {
        private readonly IGirisOlayiDal _girisOlayiDal;

        public GirisOlayiServis(IGirisOlayiDal girisOlayiDal)
        {
            _girisOlayiDal = girisOlayiDal;
        }

        public async Task<IResult> Ekle(GirisOlayi girisOlayi)
        {
            await _girisOlayiDal.Ekle(girisOlayi);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(GirisOlayi girisOlayi)
        {
            await _girisOlayiDal.Guncelle(girisOlayi);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(GirisOlayi girisOlayi)
        {
            await _girisOlayiDal.Sil(girisOlayi);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }

        public async Task<IDataResult<List<GirisOlayi>>> HepsiniGetir()
        {
            var liste = await _girisOlayiDal.HepsiniGetir();
            return new SuccessDataResult<List<GirisOlayi>>(liste, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<GirisOlayi>> IdIleGetir(int id)
        {
            var girisOlayi = await _girisOlayiDal.Getir(x => x.Id == id);
            return new SuccessDataResult<GirisOlayi>(girisOlayi, Mesajlar.IdIleGetirmeBasarili);
        }
    }

}
