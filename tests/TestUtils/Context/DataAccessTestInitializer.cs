using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestUtils.Context
{
    public static class TestInitializer
    {
        public static ServiceProvider InitializeServices()
        {
            string databaseName = Guid.NewGuid().ToString();
            var services = new ServiceCollection();

            // Register DbContext with InMemoryDatabase for testing purposes
            services.AddDbContext<FruitDbContext>(options =>
                options.UseInMemoryDatabase(databaseName));

            // Register repositories
            services.AddTransient<IFruitRepository, FruitRepository>();

            return services.BuildServiceProvider();
        }
    }
}
