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
    public interface ILimitArtirmaDal:IEntityRepository<LimitArtirma>
    {
        Task<List<LimitArtirmaDto>> KartLimitIstekleriGetir();

        Task<bool> LimitDurumGuncelle(int Id, string yeniDurum);

        Task<LimitArtirmaDto?> KartLimitIstekGetirIdIle(int id);
    }
}
