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
        IKartDal _kartDal;

        public KartServis(IKartDal kartDal)
        {
            _kartDal = kartDal;
        }

        public IResult Ekle(Kart kart)
        {
            _kartDal.Ekle(kart);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public IResult Guncelle(Kart kart)
        {
            _kartDal.Guncelle(kart);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public IDataResult<List<Kart>> HepsiniGetir()
        {
            return new SuccessDataResult<List<Kart>>(
                _kartDal.HepsiniGetir(),
                Mesajlar.HepsiniGetirmeBasarili
            );
        }

        public IDataResult<Kart> IdIleGetir(int id)
        {
            return new SuccessDataResult<Kart>(
                _kartDal.Getir(k => k.Id == id),
                Mesajlar.IdIleGetirmeBasarili
            );
        }

        public IResult Sil(Kart kart)
        {
            _kartDal.Sil(kart);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }
}
