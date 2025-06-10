using Banka.Cekirdek.YardımcıHizmetler.Transaction;
using Banka.VeriErisim.Somut.EntityFramework;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.VeriErisimi.Somut.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly BankaContext _context;
        private IDbContextTransaction _transaction;

        public EfUnitOfWork(BankaContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
                await _transaction.DisposeAsync();
        }
    }
}
