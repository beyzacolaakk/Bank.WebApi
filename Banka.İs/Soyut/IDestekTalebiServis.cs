using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Soyut
{
    public interface IDestekTalebiServis
    {
        Task<IDataResult<List<DestekTalebi>>> HepsiniGetir(); 

        Task<IResult> Ekle(DestekTalebi destekTalebi); 

        Task<IResult> Guncelle(DestekTalebi destekTalebi);  

        Task<IResult> Sil(int id);

        Task<IResult> DestekTalebiOlustur(DestekTalebiOlusturDto destekTalebiOlusturDto);
        Task<IDataResult<DestekTalebi>> IdIleGetir(int id);
        Task<IResult> DestekTalebiGuncelle(int id);
        Task<IDataResult<List<DestekTalebi>>> IdIleHepsiniGetir(int kullaniciId);

        Task<IDataResult<List<DestekTalebiOlusturDto>>> DestekIstekleriGetir();

        Task<IResult> DestekTalebiDurumGuncelle(DestekTalebiGuncelleDto destekTalebiGuncelle);

        Task<IDataResult<DestekTalebiOlusturDto>> IdIleGetirDestekTalebi(int id);

        Task<IDataResult<List<DestekTalebi>>> DestekTalebiFiltre(int id, string durum, string arama);
    }
}
