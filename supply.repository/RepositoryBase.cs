using Microsoft.EntityFrameworkCore;
using Supply.Domain;
using Supply.Repository.Context;

namespace Supply.Repository {

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class {
        protected SupplyContext Db = new SupplyContext(new DbContextOptions<SupplyContext>(), null);

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

