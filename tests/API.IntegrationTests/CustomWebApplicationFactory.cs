using BusinessLogic.Mappings;
using BusinessLogic.Services;
using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //base.ConfigureWebHost(builder);
            
            builder.ConfigureServices(services =>
            {
                // Remove the existing DbContext registration
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FruitDbContext>));
                if(descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add a new DbContext registration with InMemory database
                services.AddDbContext<FruitDbContext>(
                    options =>
                    {
                        options.UseInMemoryDatabase("E2ETests");
                    });

                services.AddAutoMapper(typeof(BusinessLogicMappingProfile));

                services.AddScoped<IFruitService, FruitService>();
                services.AddScoped<IFruitRepository, FruitRepository>();

                var serviceProvider = services.BuildServiceProvider();
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<FruitDbContext>();

                    db.Database.EnsureCreated();
                }
            });
        }
    }
}
