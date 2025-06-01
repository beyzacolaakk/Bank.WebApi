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
    public class KartIslemServis : IKartIslemServis
    {
        private readonly IKartIslemDal _kartIslemDal;

        public KartIslemServis(IKartIslemDal kartIslemDal)
        {
            _kartIslemDal = kartIslemDal;
        }

        public async Task<IResult> Ekle(KartIslem kartIslem)
        {
            await _kartIslemDal.Ekle(kartIslem);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(KartIslem kartIslem)
        {
            await _kartIslemDal.Guncelle(kartIslem);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IDataResult<List<KartIslem>>> HepsiniGetir()
        {
            var veriler = await _kartIslemDal.HepsiniGetir();
            return new SuccessDataResult<List<KartIslem>>(veriler, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<KartIslem>> IdIleGetir(int id)
        {
            var veri = await _kartIslemDal.Getir(k => k.Id == id);
            return new SuccessDataResult<KartIslem>(veri, Mesajlar.IdIleGetirmeBasarili);
        }

        public async Task<IResult> Sil(KartIslem kartIslem)
        {
            await _kartIslemDal.Sil(kartIslem);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }

}
