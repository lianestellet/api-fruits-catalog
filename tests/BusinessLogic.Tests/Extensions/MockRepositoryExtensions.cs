using Entities.Domain;
using Entities.DTOs;
using Entities.Exceptions;
using Entities.Interfaces;
using Moq;

namespace BusinessLogic.UnitTests.Extensions
{
    public static class MockRepositoryExtensions
    {
        public static void StubFindAllFruitsAsync(this Mock<IFruitRepository> mockRepository, List<Fruit> fruits)
        {
            mockRepository.Setup(r => r.FindAllFruitsAsync()).ReturnsAsync(fruits);
        }

        public static void StubFindFruitByIdAsyncAs(this Mock<IFruitRepository> mockRepository, Fruit? fruit) 
        { 
            mockRepository.Setup(r => r.FindFruitByIdAsync(It.IsAny<long>())).ReturnsAsync(fruit); 
        }

        public static void StubFindFruitTypeByIdAsyncAs(this Mock<IFruitRepository> mockRepository, FruitType? fruitType) 
        { 
            mockRepository.Setup(r => r.FruitTypeByIdAsync(It.IsAny<long>())).ReturnsAsync(fruitType); 
        }

        public static void StubSaveFruitAsyncAs(this Mock<IFruitRepository> mockRepository, Fruit fruit)
        {
            mockRepository.Setup(r => r.SaveFruitAsync(It.IsAny<Fruit>())).ReturnsAsync(fruit);
        }

        public static void StubUpdateFruitAsyncAs(this Mock<IFruitRepository> mockRepository, Fruit fruit)
        {
            mockRepository.Setup(r => r.UpdateFruitAsync(It.IsAny<Fruit>())).ReturnsAsync(fruit);
        }

        public static void StubUpdateFruitAsyncFruitNotFoundException(this Mock<IFruitRepository> mockRepository, FruitDTO fruit)
        {
            var exception = new NotFoundException(ExceptionMessages.FruitNotFoundById(fruit.Id));
            mockRepository.Setup(r => r.UpdateFruitAsync(It.IsAny<Fruit>())).ThrowsAsync(exception);
        }
    }
}
