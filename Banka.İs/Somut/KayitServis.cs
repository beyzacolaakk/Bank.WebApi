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
    public class KayitServis : IKayitServis
    {
        private readonly IKayitDal _kayitDal;

        public KayitServis(IKayitDal kayitDal)
        {
            _kayitDal = kayitDal;
        }

        public async Task<IResult> Ekle(Kayit kayit)
        {
            await _kayitDal.Ekle(kayit);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(Kayit kayit)
        {
            await _kayitDal.Guncelle(kayit);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IDataResult<List<Kayit>>> HepsiniGetir()
        {
            var veriler = await _kayitDal.HepsiniGetir();
            return new SuccessDataResult<List<Kayit>>(veriler, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<Kayit>> IdIleGetir(int id)
        {
            var veri = await _kayitDal.Getir(k => k.Id == id);
            return new SuccessDataResult<Kayit>(veri, Mesajlar.IdIleGetirmeBasarili);
        }

        public async Task<IResult> Sil(Kayit kayit)
        {
            await _kayitDal.Sil(kayit);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }

}
