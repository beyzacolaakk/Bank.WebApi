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
        private readonly IKullaniciRolDal _kullaniciRolDal;

        public KullaniciRolServis(IKullaniciRolDal kullaniciRolDal)
        {
            _kullaniciRolDal = kullaniciRolDal;
        }

        public async Task<IResult> Ekle(KullaniciRol kullaniciRol)
        {
            await _kullaniciRolDal.Ekle(kullaniciRol);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(KullaniciRol kullaniciRol)
        {
            await _kullaniciRolDal.Guncelle(kullaniciRol);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IDataResult<List<KullaniciRol>>> HepsiniGetir()
        {
            var roller = await _kullaniciRolDal.HepsiniGetir();
            return new SuccessDataResult<List<KullaniciRol>>(roller, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<KullaniciRol>> IdIleGetir(int id)
        {
            var rol = await _kullaniciRolDal.Getir(kr => kr.Id == id);
            return new SuccessDataResult<KullaniciRol>(rol, Mesajlar.IdIleGetirmeBasarili);
        }

        public async Task<IResult> Sil(KullaniciRol kullaniciRol)
        {
            await _kullaniciRolDal.Sil(kullaniciRol);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }

}
