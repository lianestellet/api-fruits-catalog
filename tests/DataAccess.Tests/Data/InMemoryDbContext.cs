using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Tests.Data
{
    public class InMemoryDbContext
    {
        private readonly FruitDbContext _context;        

        private InMemoryDbContext()
        {
            string dbName = Guid.NewGuid().ToString();
            var _dbContextOptions =
                new DbContextOptionsBuilder<FruitDbContext>()
                    .UseInMemoryDatabase(dbName)
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                    .Options;

            _context = new FruitDbContext(_dbContextOptions);
        }

        public static InMemoryDbContext Empty()
        {
            return new InMemoryDbContext();
        }

        public static async Task<InMemoryDbContext> SeedDatabaseAsync(IDbSeedData seedData)
        {
            var db = new InMemoryDbContext();
            await db.SeedDataAsync(seedData);
            return db;
        }

        public async Task<InMemoryDbContext> SeedDataAsync(IDbSeedData seedData)
        {
            var fruitTypes = seedData.SetFruitTypes();
            var fruits = seedData.SetFruits();

            if (fruitTypes.Count > 0 && !await _context.FruitTypes.AnyAsync())
                await _context.FruitTypes.AddRangeAsync(seedData.SetFruitTypes());

            await _context.SaveChangesAsync();

            if (fruits.Count > 0 && !await _context.Fruits.AnyAsync())
                await _context.Fruits.AddRangeAsync(seedData.SetFruits());

            await _context.SaveChangesAsync();
            return this;
        }

        public FruitRepository CreateRepository() => new(_context);

        public FruitDbContext GetContext() => _context;
    }
}
