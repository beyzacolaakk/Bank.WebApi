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
        Task<List<T>> HepsiniGetir(Expression<Func<T, bool>> filter = null);

        Task<T> Getir(Expression<Func<T, bool>> filter = null);

        Task Ekle(T entity);

        Task Sil(T entity);

        Task Guncelle(T entity); 
    }
}
