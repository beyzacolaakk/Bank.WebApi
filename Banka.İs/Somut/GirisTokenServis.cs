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
        private readonly IGirisTokenDal _girisTokenDal;

        public GirisTokenServis(IGirisTokenDal girisTokenDal)
        {
            _girisTokenDal = girisTokenDal;
        }

        public async Task<IResult> Ekle(GirisToken girisToken)
        {
            await _girisTokenDal.Ekle(girisToken);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(GirisToken girisToken)
        {
            await _girisTokenDal.Guncelle(girisToken);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(GirisToken girisToken)
        {
            await _girisTokenDal.Sil(girisToken);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }

        public async Task<IDataResult<List<GirisToken>>> HepsiniGetir()
        {
            var liste = await _girisTokenDal.HepsiniGetir();
            return new SuccessDataResult<List<GirisToken>>(liste, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<GirisToken>> IdIleGetir(int id)
        {
            var veri = await _girisTokenDal.Getir(g => g.Id == id);
            return new SuccessDataResult<GirisToken>(veri, Mesajlar.IdIleGetirmeBasarili);
        }
    }

}
