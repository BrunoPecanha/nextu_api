using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using uff.Infra.Context;
using uff.Infra;
using uff.Service;
using Microsoft.AspNetCore.Identity;
using uff.Domain.Entity;
using uff.domain.Repository;
using uff.domain.Services;

namespace uff.infra.dependecyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UffContext>(o => o.UseNpgsql(connectionString));
            services.AddTransient<IUffContext, UffContext>();

            //Entidadades
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();


            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
