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
    public class KullaniciServis : IKullaniciServis
    {
        IKullaniciDal _kullaniciDal; 

        public KullaniciServis(IKullaniciDal kullaniciDal)  
        {
            _kullaniciDal = kullaniciDal;
        }

        public IResult Ekle(Kullanici kullanici)
        {
            _kullaniciDal.Ekle(kullanici);
            return new SuccessResult(Mesajlar.KullaniciEklemeBasarili);
        }
        public IResult Sil(Kullanici kullanici)
        {
            _kullaniciDal.Sil(kullanici);
            return new SuccessResult(Mesajlar.KullaniciSilmeBasarili);
        }

        public IResult Guncelle(Kullanici kullanici)
        {
            _kullaniciDal.Guncelle(kullanici);
            return new SuccessResult(Mesajlar.KullaniciGuncellemeBasarili);
        }
        public Kullanici MaileGoreGetir(string telefon) 
        {
            var result = _kullaniciDal.Getir(u => u.Telefon == telefon);
            return result;
        }
        public IDataResult<List<Kullanici>> HepsiniGetir()
        {
            return new SuccessDataResult<List<Kullanici>>(_kullaniciDal.HepsiniGetir(), "Kullanıcılar Getirildi");
        }

        public IDataResult<Kullanici> IdIleGetir(int id)
        {
            return new SuccessDataResult<Kullanici>( _kullaniciDal.Getir(u => u.Id == id), "Kullanıcı Getirildi");
        }

        public List<Rol> YetkileriGetir(Kullanici kullanici)
        {
            return _kullaniciDal.YetkileriGetir(kullanici);
        }
    }
}
