﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Infra.Context;
using UFF.Service;

namespace UFF.Infra.dependecyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UffContext>(o => o.UseNpgsql(connectionString));
            services.AddTransient<IUffContext, UffContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();


            //Entidadades
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IFavoriteStoreRepository, FavoriteStoreRepository>();
            services.AddTransient<IFavoriteProfessionalRepository, FavoriteProfessionalRepository>();

            services.AddTransient<IEmployeeStoreRepository, EmployeeStoreRepository>();
            services.AddTransient<IEmployeeStoreService, EmployeeStoreService>();
            services.AddTransient<IQueueRepository, QueueRepository>();
            services.AddTransient<IQueueService, QueueService>();
            services.AddTransient<IFileService, FileService>();

            services.AddTransient<ICustomerService,  Service.CustomerService>();
            services.AddTransient<IFavoriteService, FavoriteService>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<INotificationService, NotificationService>();

            services.AddTransient<IStoreRatingRepository, StoreRatingRepository>();
            services.AddTransient<IServiceService, ServiceService>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IStoreRatingService, StoreRatingService>();
            services.AddTransient<IServiceRepository, ServiceRepository>();

            services.AddTransient<ICustomerServiceRepository, CustomerServiceRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            services.AddTransient<IServiceCategoryRepository, ServiceCategoryRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
