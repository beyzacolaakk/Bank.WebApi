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
    public class GirisTokenServis : IGirisTokenServis
    {
        IGirisTokenDal _girisTokenDal;

        public GirisTokenServis(IGirisTokenDal girisTokenDal)
        {
            _girisTokenDal = girisTokenDal;
        }

        public IResult Ekle(GirisToken girisToken)
        {
            _girisTokenDal.Ekle(girisToken);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public IResult Guncelle(GirisToken girisToken)
        {
            _girisTokenDal.Guncelle(girisToken);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public IDataResult<List<GirisToken>> HepsiniGetir()
        {
            return new SuccessDataResult<List<GirisToken>>(
                _girisTokenDal.HepsiniGetir(),
                Mesajlar.HepsiniGetirmeBasarili
            );
        }

        public IDataResult<GirisToken> IdIleGetir(int id)
        {
            return new SuccessDataResult<GirisToken>(
                _girisTokenDal.Getir(g => g.Id == id),
                Mesajlar.IdIleGetirmeBasarili
            );
        }

        public IResult Sil(GirisToken girisToken)
        {
            _girisTokenDal.Sil(girisToken);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }
}
