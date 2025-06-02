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
        Task<IDataResult<Hesap>> HesapNoIdIleGetir(int id);
        Task<IDataResult<Hesap>> IdIleGetir(int id);
        Task<IResult> ParaTransferi(int gonderenHesapId, int aliciHesapId, decimal miktar);
        Task<IResult> OtomatikHesapOlustur(HesapOlusturDto hesapOlusturDto);

        Task<Hesap> KullaniciHesapGetirIlkVadesiz(int kullaniciId); 
    }
}
