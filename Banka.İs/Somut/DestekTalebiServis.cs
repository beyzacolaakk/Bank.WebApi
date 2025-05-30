
using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class DestekTalebiServis : IDestekTalebiServis
    {
        IDestekTalebiDal _destekTalebiDal;

        public DestekTalebiServis(IDestekTalebiDal destekTalebiDal)
        {
            _destekTalebiDal = destekTalebiDal;
        }

        public IResult Ekle(DestekTalebi destekTalebi)
        {
            _destekTalebiDal.Ekle(destekTalebi);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public IResult Guncelle(DestekTalebi destekTalebi)
        {
            _destekTalebiDal.Guncelle(destekTalebi);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public IDataResult<List<DestekTalebi>> HepsiniGetir()
        {
            return new SuccessDataResult<List<DestekTalebi>>(_destekTalebiDal.HepsiniGetir(), Mesajlar.HepsiniGetirmeBasarili);
        }

        public IDataResult<DestekTalebi> IdIleGetir(int id)
        {
            return new SuccessDataResult<DestekTalebi>(_destekTalebiDal.Getir(u => u.Id == id), Mesajlar.IdIleGetirmeBasarili);
        }

        public IResult Sil(DestekTalebi destekTalebi)
        {
            _destekTalebiDal.Sil(destekTalebi);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }
}
