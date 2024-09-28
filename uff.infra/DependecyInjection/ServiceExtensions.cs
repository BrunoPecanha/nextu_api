using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using uff.Domain;
using uff.Infra.Context;
using uff.Infra;
using uff.Service;

namespace uff.infra.dependecyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UffContext>(o => o.UseNpgsql(connectionString));
            services.AddTransient<IUffContext, UffContext>();

            //Entidadades
            services.AddTransient<ICostumerService, CostumerService>();
            services.AddTransient<ICostumerRepository, CostumerRepository>();

            return services;
        }
    }
}
