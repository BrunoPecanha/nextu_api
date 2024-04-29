using Microsoft.EntityFrameworkCore;
using uff.Domain;
using uff.Repository.Context;

namespace uff.Repository {

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class {
        protected UffContext Db = new UffContext(new DbContextOptions<UffContext>(), null);

        public void Add(T obj) {
            Db.Set<T>().Add(obj);
            Db.SaveChanges();
        }

        public void Dispose() {
            Db.Dispose();
        }
        public void Remove(T obj) {
            Db.Set<T>().Remove(obj);
            Db.SaveChanges();
        }

        public void Update(T obj) {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}

