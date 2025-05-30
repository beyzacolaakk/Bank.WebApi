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
        IGirisOlayiDal _girisOlayiDal;

        public GirisOlayiServis(IGirisOlayiDal girisOlayiDal)
        {
            _girisOlayiDal = girisOlayiDal;
        }

        public IResult Ekle(GirisOlayi girisOlayi)
        {
            _girisOlayiDal.Ekle(girisOlayi);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public IResult Guncelle(GirisOlayi girisOlayi)
        {
            _girisOlayiDal.Guncelle(girisOlayi);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public IDataResult<List<GirisOlayi>> HepsiniGetir()
        {
            return new SuccessDataResult<List<GirisOlayi>>(_girisOlayiDal.HepsiniGetir(), Mesajlar.HepsiniGetirmeBasarili);
        }

        public IDataResult<GirisOlayi> IdIleGetir(int id)
        {
            return new SuccessDataResult<GirisOlayi>(_girisOlayiDal.Getir(x => x.Id == id), Mesajlar.IdIleGetirmeBasarili);
        }

        public IResult Sil(GirisOlayi girisOlayi)
        {
            _girisOlayiDal.Sil(girisOlayi);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }
}
