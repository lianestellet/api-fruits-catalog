using Entities.Domain;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace FruitCatalog.Tests
{
    public static class MockFruitContext
    {
        public static FruitContext GetMockedDbContext()
        {
            var options = new DbContextOptionsBuilder<FruitContext>()
                .UseInMemoryDatabase(databaseName: "TestFruitDatabase")
                .Options;

            var context = new FruitContext(options);

            // Seed the database with initial data
            SeedData(context);

            return context;
        }

        private static void SeedData(FruitContext context)
        {
            var fruitTypes = new List<FruitType>
            {
                new FruitType { Id = 1, Name = "Citrus", Description = "Citrus fruits" },
                new FruitType { Id = 2, Name = "Berry", Description = "Berry fruits" }
            };

            var fruits = new List<Fruit>
            {
                new() {FruitTypeId = 1,Name = "Orange",Description = "A sweet orange fruit"},
                new() { Name = "Strawberry", FruitTypeId = 2, Description = "A delicious red berry" }
            };

            context.FruitTypes.AddRange(fruitTypes);
            context.Fruits.AddRange(fruits);

            context.SaveChanges();
        }
    }
}
