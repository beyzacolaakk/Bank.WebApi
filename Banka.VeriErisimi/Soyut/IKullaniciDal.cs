using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.VeriErisimi;
using Banka.Varlıklar.Somut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.VeriErisimi.Soyut
{
    public interface IKullaniciDal : IEntityRepository<Kullanici>
    {
        List<Rol> YetkileriGetir(Kullanici kullanici);
       // List<UsersDetailDto> KullaniciDetaylariniGetir();


       // UserById IdIleKullaniciGetir(int id);


        Kullanici EkleVeIdGetir(Kullanici kullanici);


    }
}
