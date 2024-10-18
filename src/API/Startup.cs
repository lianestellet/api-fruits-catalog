using Entities.Interfaces;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using DataAccess.Repositories;

namespace FruitCatalog.API
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var databaseProvider = Configuration["DatabaseProvider"];

            if (databaseProvider == "pgsql")
            {
                services.AddDbContext<FruitDbContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("PostgresConnection")));
            }
            else if (databaseProvider == "InMemory")
            {
                services.AddDbContext<FruitDbContext>(opt =>
                    opt.UseInMemoryDatabase("FruitCatalogInMemoryDb"));
            }           

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IFruitService, FruitService>();
            services.AddScoped<IFruitRepository, FruitRepository>();            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FruitCatalog API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}