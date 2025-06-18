
using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class DestekTalebiServis : IDestekTalebiServis
    {
        private readonly IDestekTalebiDal _destekTalebiDal;


        public DestekTalebiServis(IDestekTalebiDal destekTalebiDal)
        {
            _destekTalebiDal = destekTalebiDal;


        }

        public async Task<IResult> Ekle(DestekTalebi destekTalebi)
        {
            await _destekTalebiDal.Ekle(destekTalebi);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(DestekTalebi destekTalebi)
        {
            await _destekTalebiDal.Guncelle(destekTalebi);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(int id)
        {
            var destekTalebi = IdIleGetir(id).Result.Data;
            await _destekTalebiDal.Sil(destekTalebi);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
        public async Task<IResult> DestekTalebiOlustur(DestekTalebiOlusturDto destekTalebiOlusturDto)
        {
            var destekTalebi= new DestekTalebi{
                Durum="Açık",
                Konu=destekTalebiOlusturDto.Konu,
                KullaniciId=destekTalebiOlusturDto.KullaniciId, 
                OlusturmaTarihi=DateTime.Now,
                Mesaj=destekTalebiOlusturDto.Mesaj,
                Kategori=destekTalebiOlusturDto.Kategori,
                
                
            };
            await _destekTalebiDal.Ekle(destekTalebi);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }
        public async Task<IResult> DestekTalebiGuncelle(int id) 
        {
            await _destekTalebiDal.DurumuGuncelle(id, "Tamamlandı"); 
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }
        public async Task<IDataResult<List<DestekTalebi>>> HepsiniGetir()
        {
            var liste = await _destekTalebiDal.HepsiniGetir();
            return new SuccessDataResult<List<DestekTalebi>>(liste, Mesajlar.HepsiniGetirmeBasarili);
        }

        public async Task<IDataResult<List<DestekTalebi>>> IdIleHepsiniGetir(int kullaniciId) 
        {
            var liste = await _destekTalebiDal.HepsiniGetir(x => x.KullaniciId == kullaniciId);
            return new SuccessDataResult<List<DestekTalebi>>(liste, Mesajlar.IdIleGetirmeBasarili);
        }

        public async Task<IDataResult<List<DestekTalebiOlusturDto>>> DestekIstekleriGetir()
        {
            var liste = await _destekTalebiDal.DestekTalebleriGetir();
            return new SuccessDataResult<List<DestekTalebiOlusturDto>>(liste, Mesajlar.IdIleGetirmeBasarili);
        }


        public async Task<IDataResult<DestekTalebi>> IdIleGetir(int id)
        {
            var destekTalebi = await _destekTalebiDal.Getir(x => x.Id == id);
            return new SuccessDataResult<DestekTalebi>(destekTalebi, Mesajlar.IdIleGetirmeBasarili);
        }

        public async Task<IResult> DestekTalebiDurumGuncelle(DestekTalebiGuncelleDto destekTalebiGuncelle)  
        {
            var veri = await _destekTalebiDal.DestekTalebiDurumGuncelle(destekTalebiGuncelle.Id, destekTalebiGuncelle.Durum!,destekTalebiGuncelle.Yanit);
            if (veri)
            {
                return new SuccessResult(Mesajlar.GuncellemeBasarili);
            }
            else
            {
                return new ErrorResult(Mesajlar.GuncellemeBasarisiz);
            }
        }

        public async Task<IDataResult<DestekTalebiOlusturDto>> IdIleGetirDestekTalebi(int id)
        {
            var destek = await _destekTalebiDal.DestekTalebiGetirIdIle(id);
            return new SuccessDataResult<DestekTalebiOlusturDto>(destek, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<IDataResult<List<DestekTalebi>>> DestekTalebiFiltre(int id, string durum, string arama)
        {
    
            var liste = await _destekTalebiDal.HepsiniGetir(x => x.KullaniciId == id);

            var filtrelenmis = liste.AsQueryable();

            if (!string.Equals(durum, "tum", StringComparison.OrdinalIgnoreCase))
            {
                filtrelenmis = filtrelenmis.Where(t => t.Durum.Equals(durum, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(arama))
            {
                filtrelenmis = filtrelenmis.Where(t =>
                    t.Konu.Contains(arama, StringComparison.OrdinalIgnoreCase) ||
                    t.Mesaj.Contains(arama, StringComparison.OrdinalIgnoreCase));
            }

            var sonuc = filtrelenmis.OrderByDescending(t => t.OlusturmaTarihi).ToList();

            return new SuccessDataResult<List<DestekTalebi>>(sonuc);
        }

    }


}


