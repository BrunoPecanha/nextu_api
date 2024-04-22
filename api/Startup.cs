using WeApi.Config;
using WeApi.Options;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Supply.Repository.Context;
using Supply.Service.Extensions;
using System;

namespace WeApi {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var dbConnectionString = Configuration.GetConnectionString("sqlLiteConnection");
            services.AddDbContext<SupplyContext>(options => options.UseSqlite(dbConnectionString));
            services.RegisterServices(Configuration.GetConnectionString(dbConnectionString));
            services.AddControllers();
            services.AddSwaggerGen();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfigProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });


            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger();
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);

            });

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
