using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using uff.Domain;
using uff.Repository;
using uff.Repository.Context;

namespace uff.Service.Extensions {
    public static class ServiceExtensions {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString) {

            services.AddDbContext<UffContext>(o => o.UseSqlite(connectionString));
            services.AddTransient<IUffContext, UffContext>();

            //Entidadades
            services.AddTransient<ICostumerService, CostumerService>();
            services.AddTransient<ICostumerRepository, CostumerRepository>();        

            return services;
        }
    }
}
