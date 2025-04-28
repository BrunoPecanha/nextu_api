using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UFF.Infra.Context;
using UFF.Infra;
using UFF.Service;
using Microsoft.AspNetCore.Identity;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Domain.Services;

namespace UFF.Infra.dependecyInjection
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
