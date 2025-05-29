using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UFF.Domain.Repository;
using UFF.Infra.Context;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly UffContext Db;

    public RepositoryBase(UffContext context)
    {
        Db = context;
    }

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
        Db.Set<T>().Update(obj);
    }

    public async Task SaveChangesAsync()
    {
        await Db.SaveChangesAsync();
    }
}
