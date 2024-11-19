using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.UnitTests
{
    public static class TestInitializer
    {
        public static ServiceProvider InitializeServices()
        {
            var services = new ServiceCollection();

            // Register DbContext with InMemoryDatabase for testing purposes
            services.AddDbContext<FruitDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: $"FruitCatalogTest_{System.Guid.NewGuid()}"));

            // Register repositories and services
            services.AddTransient<IFruitRepository, FruitRepository>();

            return services.BuildServiceProvider();
        }
    }
}

