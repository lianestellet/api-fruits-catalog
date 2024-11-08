using BusinessLogic.Services;
using Entities.Validation;
using TestUtils.DataAccess.Context;

namespace BusinessLogic.IntegrationTests.Extensions
{
    public static class SetupInMemoryDbExtensions
    {
        public static FruitService GetFruitService(this SetupInMemoryDb database)
        {
            FruitDTOValidator _validator = new();
            CreateFruitDTOValidator _createValidator = new();
            var repository = database.CreateRepository();
            return new FruitService(repository, _validator, _createValidator);
        }
    }
}
