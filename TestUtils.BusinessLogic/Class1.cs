using BusinessLogic.Services;
using Entities.Validation;
using TestUtils.Core.Data;
using TestUtils.DataAccess.Context;

namespace TestUtils.BusinessLogic
{
    public static class CreateFruitService
    {
        public static async Task<FruitService> WithSeedData(ISeedData seedData)
        {
            var _validator = new FruitDTOValidator();
            var _createValidator = new CreateFruitDTOValidator();
            var inMemoryDbContext = await InMemoryDbContext.SeedDatabaseAsync(seedData);
            var fruitRepository = inMemoryDbContext.CreateRepository();

            return new FruitService(fruitRepository, _validator, _createValidator);
        }
        public static async Task<FruitService> WithSeedData(ISeedData seedData)
        {
            var _validator = new FruitDTOValidator();
            var _createValidator = new CreateFruitDTOValidator();
            var inMemoryDbContext = await InMemoryDbContext.SeedDatabaseAsync(seedData);
            var fruitRepository = inMemoryDbContext.CreateRepository();

            return new FruitService(fruitRepository, _validator, _createValidator);
        }

    }
}
