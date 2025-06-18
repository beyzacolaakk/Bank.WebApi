using Banka.Cekirdek.VeriErisimi;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.VeriErisimi.Soyut
{
    public interface IDestekTalebiDal : IEntityRepository<DestekTalebi>
    {

        Task DurumuGuncelle(int id, string yeniDurum);

        Task<List<DestekTalebiOlusturDto>> DestekTalebleriGetir();

        Task<bool> DestekTalebiDurumGuncelle(int Id, string yeniDurum,string yanit);

        Task<DestekTalebiOlusturDto?> DestekTalebiGetirIdIle(int id);
    }
}
