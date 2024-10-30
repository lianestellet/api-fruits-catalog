using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess.Context
{
    public class FruitDbContextFactory : IDesignTimeDbContextFactory<FruitDbContext>
    {
        public FruitDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FruitDbContext>();
            var connectionString = "Host=wsl.localhost;Database=fruit_catalog_db;Username=postgres;Password=postgres";
            optionsBuilder.UseNpgsql(connectionString);

            return new FruitDbContext(optionsBuilder.Options);
        }
    }
}