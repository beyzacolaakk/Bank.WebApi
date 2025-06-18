using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IHesapServis
    {
        Task<IDataResult<List<Hesap>>> HepsiniGetir();

        Task<IResult> Ekle(Hesap hesap);

        Task<IResult> Guncelle(Hesap hesap);

        Task<IResult> Sil(Hesap hesap);
        Task<IDataResult<Hesap>> HesapNoIdIleGetir(string id);
        Task<IDataResult<Hesap>> IdIleGetir(int id);
        Task<IDataResult<decimal>> ParaTransferi(string gonderenHesapId, string aliciHesapId, decimal miktar);

        Task<IDataResult<decimal>> ParaCekYatir(ParaCekYatirDto paraCekYatirDto);
        Task<IResult> OtomatikHesapOlustur(HesapOlusturDto hesapOlusturDto);

        Task<Hesap> KullaniciHesapGetirIlkVadesiz(int kullaniciId);

        Task<IDataResult<List<Hesap>>> IdIleHepsiniGetir(int id);

        Task<IDataResult<VarlıklarDto>> VarliklariGetirAsync(int kullaniciId);

        Task<List<int>> GetirKullaniciyaAitHesapIdler(int kullaniciId);

        Task<IDataResult<List<HesapIstekleriDto>>> HesapIstekleriGetir(); 
        Task<IDataResult<IstekSayilariDto>> IsteklSayilariGetir();

        Task<IResult> HesapDurumGuncelle(DurumuGuncelleDto durumGuncelleDto);

        Task<IDataResult<HesapIstekleriDto>> HesapIstekleriIdIleGetir(int id);
    }
}
