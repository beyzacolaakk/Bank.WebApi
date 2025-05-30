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
        IKayitDal _kayitDal;

        public KayitServis(IKayitDal kayitDal)
        {
            _kayitDal = kayitDal;
        }

        public IResult Ekle(Kayit kayit)
        {
            _kayitDal.Ekle(kayit);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public IResult Guncelle(Kayit kayit)
        {
            _kayitDal.Guncelle(kayit);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public IDataResult<List<Kayit>> HepsiniGetir()
        {
            return new SuccessDataResult<List<Kayit>>(
                _kayitDal.HepsiniGetir(),
                Mesajlar.HepsiniGetirmeBasarili
            );
        }

        public IDataResult<Kayit> IdIleGetir(int id)
        {
            return new SuccessDataResult<Kayit>(
                _kayitDal.Getir(k => k.Id == id),
                Mesajlar.IdIleGetirmeBasarili
            );
        }

        public IResult Sil(Kayit kayit)
        {
            _kayitDal.Sil(kayit);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }
}
