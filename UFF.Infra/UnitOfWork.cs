using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using UFF.Domain.Repository;
using UFF.Infra.Context;

namespace UFF.Infra
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly UffContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(UffContext context)
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
            await _transaction?.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction?.RollbackAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
