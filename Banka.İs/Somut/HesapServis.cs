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
    public class HesapServis : IHesapServis
    {
        private readonly IHesapDal _hesapDal;

        public HesapServis(IHesapDal hesapDal)
        {
            _hesapDal = hesapDal;
        }

        public async Task<IResult> Ekle(Hesap hesap)
        {
            await _hesapDal.Ekle(hesap);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(Hesap hesap)
        {
            await _hesapDal.Guncelle(hesap);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(Hesap hesap)
        {
            await _hesapDal.Sil(hesap);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }

        public async Task<IDataResult<List<Hesap>>> HepsiniGetir()
        {
            var hesaplar = await _hesapDal.HepsiniGetir();
            return new SuccessDataResult<List<Hesap>>(hesaplar, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<Hesap>> IdIleGetir(int id)
        {
            var hesap = await _hesapDal.Getir(h => h.Id == id);
            return new SuccessDataResult<Hesap>(hesap, Mesajlar.IdIleGetirmeBasarili);
        }
    }

}
