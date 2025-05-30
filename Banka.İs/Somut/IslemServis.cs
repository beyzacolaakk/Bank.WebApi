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
    public class IslemServis : IIslemServis
    {
        IIslemDal _islemDal;

        public IslemServis(IIslemDal islemDal)
        {
            _islemDal = islemDal;
        }

        public IResult Ekle(Islem islem)
        {
            _islemDal.Ekle(islem);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public IResult Guncelle(Islem islem)
        {
            _islemDal.Guncelle(islem);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public IDataResult<List<Islem>> HepsiniGetir()
        {
            return new SuccessDataResult<List<Islem>>(
                _islemDal.HepsiniGetir(),
                Mesajlar.HepsiniGetirmeBasarili
            );
        }

        public IDataResult<Islem> IdIleGetir(int id)
        {
            return new SuccessDataResult<Islem>(
                _islemDal.Getir(i => i.Id == id),
                Mesajlar.IdIleGetirmeBasarili
            );
        }

        public IResult Sil(Islem islem)
        {
            _islemDal.Sil(islem);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }
}
