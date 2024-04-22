using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Supply.Domain;
using Supply.Repository;
using Supply.Repository.Context;

namespace Supply.Service.Extensions {
    public static class ServiceExtensions {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString) {

            services.AddDbContext<SupplyContext>(o => o.UseSqlite(connectionString));
            services.AddTransient<ISupplyContext, SupplyContext>();

            //Entidadades
            services.AddTransient<ICostumerService, CostumerService>();
            services.AddTransient<ICostumerRepository, CostumerRepository>();        

            return services;
        }
    }
}
