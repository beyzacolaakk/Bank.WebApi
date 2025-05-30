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
        IHesapDal _hesapDal;

        public HesapServis(IHesapDal hesapDal)
        {
            _hesapDal = hesapDal;
        }

        public IResult Ekle(Hesap hesap)
        {
            _hesapDal.Ekle(hesap);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public IResult Guncelle(Hesap hesap)
        {
            _hesapDal.Guncelle(hesap);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public IDataResult<List<Hesap>> HepsiniGetir()
        {
            return new SuccessDataResult<List<Hesap>>(
                _hesapDal.HepsiniGetir(),
                Mesajlar.HepsiniGetirmeBasarili
            );
        }

        public IDataResult<Hesap> IdIleGetir(int id)
        {
            return new SuccessDataResult<Hesap>(
                _hesapDal.Getir(h => h.Id == id),
                Mesajlar.IdIleGetirmeBasarili
            );
        }

        public IResult Sil(Hesap hesap)
        {
            _hesapDal.Sil(hesap);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }
}
