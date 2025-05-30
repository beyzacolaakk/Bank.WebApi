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
        IKartIslemDal _kartIslemDal;

        public KartIslemServis(IKartIslemDal kartIslemDal)
        {
            _kartIslemDal = kartIslemDal;
        }

        public IResult Ekle(KartIslem kartIslem)
        {
            _kartIslemDal.Ekle(kartIslem);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public IResult Guncelle(KartIslem kartIslem)
        {
            _kartIslemDal.Guncelle(kartIslem);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public IDataResult<List<KartIslem>> HepsiniGetir()
        {
            return new SuccessDataResult<List<KartIslem>>(
                _kartIslemDal.HepsiniGetir(),
                Mesajlar.HepsiniGetirmeBasarili
            );
        }

        public IDataResult<KartIslem> IdIleGetir(int id)
        {
            return new SuccessDataResult<KartIslem>(
                _kartIslemDal.Getir(k => k.Id == id),
                Mesajlar.IdIleGetirmeBasarili
            );
        }

        public IResult Sil(KartIslem kartIslem)
        {
            _kartIslemDal.Sil(kartIslem);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }
}
