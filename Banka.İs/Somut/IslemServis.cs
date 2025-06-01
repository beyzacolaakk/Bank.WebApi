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
        private readonly IIslemDal _islemDal;

        public IslemServis(IIslemDal islemDal)
        {
            _islemDal = islemDal;
        }

        public async Task<IResult> Ekle(Islem islem)
        {
            await _islemDal.Ekle(islem);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(Islem islem)
        {
            await _islemDal.Guncelle(islem);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(Islem islem)
        {
            await _islemDal.Sil(islem);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }

        public async Task<IDataResult<List<Islem>>> HepsiniGetir()
        {
            var islemler = await _islemDal.HepsiniGetir();
            return new SuccessDataResult<List<Islem>>(islemler, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<Islem>> IdIleGetir(int id)
        {
            var islem = await _islemDal.Getir(i => i.Id == id);
            return new SuccessDataResult<Islem>(islem, Mesajlar.IdIleGetirmeBasarili);
        }
    }

}
