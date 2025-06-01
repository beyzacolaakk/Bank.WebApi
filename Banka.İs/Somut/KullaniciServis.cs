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
        private readonly IKullaniciDal _kullaniciDal;

        public KullaniciServis(IKullaniciDal kullaniciDal)
        {
            _kullaniciDal = kullaniciDal;
        }

        public async Task<IResult> Ekle(Kullanici kullanici)
        {
            await _kullaniciDal.Ekle(kullanici);
            return new SuccessResult(Mesajlar.KullaniciEklemeBasarili);
        }

        public async Task<IResult> Sil(Kullanici kullanici)
        {
            await _kullaniciDal.Sil(kullanici);
            return new SuccessResult(Mesajlar.KullaniciSilmeBasarili);
        }

        public async Task<IResult> Guncelle(Kullanici kullanici)
        {
            await _kullaniciDal.Guncelle(kullanici);
            return new SuccessResult(Mesajlar.KullaniciGuncellemeBasarili);
        }

        public async Task<Kullanici> MaileGoreGetir(string telefon)
        {
            return await _kullaniciDal.Getir(u => u.Telefon == telefon);
        }

        public async Task<IDataResult<List<Kullanici>>> HepsiniGetir()
        {
            var kullanicilar = await _kullaniciDal.HepsiniGetir();
            return new SuccessDataResult<List<Kullanici>>(kullanicilar, "Kullanıcılar Getirildi");
        }

        public async Task<IDataResult<Kullanici>> IdIleGetir(int id)
        {
            var kullanici = await _kullaniciDal.Getir(u => u.Id == id);
            return new SuccessDataResult<Kullanici>(kullanici, "Kullanıcı Getirildi");
        }

        public async Task<List<Rol>> YetkileriGetir(Kullanici kullanici)
        {
            return await _kullaniciDal.YetkileriGetir(kullanici);
        }
    }

}
