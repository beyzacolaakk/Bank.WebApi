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
    public class KartServis : IKartServis
    {
        private readonly IKartDal _kartDal;

        public KartServis(IKartDal kartDal)
        {
            _kartDal = kartDal;
        }

        public async Task<IResult> Ekle(Kart kart)
        {
            await _kartDal.Ekle(kart);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(Kart kart)
        {
            await _kartDal.Guncelle(kart);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IDataResult<List<Kart>>> HepsiniGetir()
        {
            var veriler = await _kartDal.HepsiniGetir();
            return new SuccessDataResult<List<Kart>>(veriler, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<Kart>> IdIleGetir(int id)
        {
            var veri = await _kartDal.Getir(k => k.Id == id);
            return new SuccessDataResult<Kart>(veri, Mesajlar.IdIleGetirmeBasarili);
        }

        public async Task<IResult> Sil(Kart kart)
        {
            await _kartDal.Sil(kart);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }

}
