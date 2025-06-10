using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.YardımcıHizmetler.Transaction
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
