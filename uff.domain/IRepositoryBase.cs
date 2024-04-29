namespace uff.Domain {
    public interface IRepositoryBase<T> where T : class {
        void Add(T obj);    
        void Update(T obj);
        void Dispose();
    }
}
