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
    public class SubeServis : ISubeServis
    {
        ISubeDal _subeDal;

        public SubeServis(ISubeDal subeDal)
        {
            _subeDal = subeDal;
        }

        public IResult Ekle(Sube sube)
        {
            _subeDal.Ekle(sube);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public IResult Guncelle(Sube sube)
        {
            _subeDal.Guncelle(sube);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public IDataResult<List<Sube>> HepsiniGetir()
        {
            return new SuccessDataResult<List<Sube>>(
                _subeDal.HepsiniGetir(),
                Mesajlar.HepsiniGetirmeBasarili
            );
        }

        public IDataResult<Sube> IdIleGetir(int id)
        {
            return new SuccessDataResult<Sube>(
                _subeDal.Getir(s => s.Id == id),
                Mesajlar.IdIleGetirmeBasarili
            );
        }

        public IResult Sil(Sube sube)
        {
            _subeDal.Sil(sube);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }
}
