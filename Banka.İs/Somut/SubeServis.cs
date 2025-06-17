using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
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

        public async Task<IResult> Ekle(Sube sube)
        {
            await _subeDal.Ekle(sube);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(Sube sube)
        {
            await _subeDal.Guncelle(sube);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IDataResult<List<Sube>>> HepsiniGetir()
        {
            var list = await _subeDal.HepsiniGetir();
            return new SuccessDataResult<List<Sube>>(list, Mesajlar.HepsiniGetirmeBasarili);
        }
        public async Task<IDataResult<List<SubeDto>>> SubeGetir()
        {
            var list = await _subeDal.HepsiniGetir();

            var dtoList = list.Select(s => new SubeDto
            {
                Id = s.Id,
                SubeAdi = s.SubeAdi
            }).ToList();

            return new SuccessDataResult<List<SubeDto>>(dtoList, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<Sube>> IdIleGetir(int id)
        {
            var entity = await _subeDal.Getir(s => s.Id == id);
            return new SuccessDataResult<Sube>(entity, Mesajlar.IdIleGetirmeBasarili);
        }

        public async Task<IResult> Sil(Sube sube)
        {
            await _subeDal.Sil(sube);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
    }

}
