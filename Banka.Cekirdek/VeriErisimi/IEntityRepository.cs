using Banka.Cekirdek.Varlıklar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.VeriErisimi
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> HepsiniGetir(Expression<Func<T, bool>> filter = null);

        T Getir(Expression<Func<T, bool>> filter = null); 

        void Ekle(T entity);  

        void Sil(T entity); 

        void Guncelle(T entity); 
    }
}
