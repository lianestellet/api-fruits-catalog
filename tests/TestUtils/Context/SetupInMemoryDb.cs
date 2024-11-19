using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Utils.Data;

namespace TestUtils.Context
{
    public class SetupInMemoryDb
    {
        private readonly FruitDbContext _context;

        private SetupInMemoryDb()
        {
            string dbName = Guid.NewGuid().ToString();
            var _dbContextOptions =
                new DbContextOptionsBuilder<FruitDbContext>()
                    .UseInMemoryDatabase(dbName)
                    .Options;

            _context = new FruitDbContext(_dbContextOptions);
        }

        public static SetupInMemoryDb Empty() => new();

        public static async Task<SetupInMemoryDb> SeededAsync(ISeedData seedData)
        {
            var db = Empty();
            var fruitTypes = seedData.SetFruitTypes();
            var fruits = seedData.SetFruits();

            if (fruitTypes.Count > 0 && !await db._context.FruitTypes.AnyAsync())
                await db._context.FruitTypes.AddRangeAsync(seedData.SetFruitTypes());

            await db._context.SaveChangesAsync();

            if (fruits.Count > 0 && !await db._context.Fruits.AnyAsync())
                await db._context.Fruits.AddRangeAsync(seedData.SetFruits());

            await db._context.SaveChangesAsync();
            return db;
        }

        public FruitRepository CreateRepository() => new(_context);
    }
}
