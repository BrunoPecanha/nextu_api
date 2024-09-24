using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using uff.Domain;
using uff.Infra.Context;

namespace uff.Infra
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected UffContext Db = new UffContext(new DbContextOptions<UffContext>(), null);

        public async Task AddAsync(T obj)
        {
            await Db.Set<T>().AddAsync(obj);
        }

        public async Task DisposeAsync()
        {
            await Db.DisposeAsync();
        }
        public void Remove(T obj)
        {
            Db.Set<T>().Remove(obj);
        }

        public void Update(T obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await Db.SaveChangesAsync();
        }
    }
}

