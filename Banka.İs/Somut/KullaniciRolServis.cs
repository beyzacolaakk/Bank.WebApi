using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.VeriErisimi.Soyut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class KullaniciRolServis : IKullaniciRolServis
    {
        IKullaniciRolDal _kullaniciRolDal;

        public KullaniciRolServis(IKullaniciRolDal kullaniciRolDal)
        {
            _kullaniciRolDal = kullaniciRolDal;
        }

        public IResult Ekle(KullaniciRol kullaniciRol)
        {
            _kullaniciRolDal.Ekle(kullaniciRol);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public IResult Guncelle(KullaniciRol kullaniciRol)
        {
            _kullaniciRolDal.Guncelle(kullaniciRol);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public IDataResult<List<KullaniciRol>> HepsiniGetir()
        {
            return new SuccessDataResult<List<KullaniciRol>>(
                _kullaniciRolDal.HepsiniGetir(),
                Mesajlar.HepsiniGetirmeBasarili
            );
        }

        public IDataResult<KullaniciRol> IdIleGetir(int id)
        {
            return new SuccessDataResult<KullaniciRol>(
                _kullaniciRolDal.Getir(kr => kr.Id == id),
                Mesajlar.IdIleGetirmeBasarili
            );
        }

        public IResult Sil(KullaniciRol kullaniciRol)
        {
            _kullaniciRolDal.Sil(kullaniciRol);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }
}
