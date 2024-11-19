using API.Models.Request;
using AutoMapper;
using BusinessLogic.Mappings;
using BusinessLogic.Models.DTOs;
using BusinessLogic.Models.Request;
using BusinessLogic.Services;
using TestUtils.Context;
using Utils.Data;
using Utils.Fixtures;

namespace BusinessLogic.IntegrationTests.Services
{
    public class FruitServiceIntegrationTests
    {        
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile<BusinessLogicMappingProfile>(); });
            _mapper = mappingConfig.CreateMapper();
        }

        [Test]
        public async Task FindAllFruitsAsync_ShouldReturnAllFruits_WhenCalled()
        {
            // Arrange
            var expectedFruit = CitrusFixture.Lemon;
            var seedData = new FruitSeedData(expectedFruit);
            var expectedCount = seedData.Fruits.Count;

            SetupInMemoryDb inMemoryDb = await SetupInMemoryDb.SeededAsync(seedData);
            var repository = inMemoryDb.CreateRepository();
            var service = new FruitService(repository, _mapper);

            // Act
            var fruits = (await service.FindAllFruitsAsync()).Fruits.ToList();

            // Assert
            Assert.That(fruits, Has.Count.EqualTo(expectedCount));
            Assert.That(fruits, Has.Exactly(1).Matches<FruitDTO>(f => f.Name == expectedFruit.Name));
        }

        [Test]
        public async Task FindFruitByIdAsync_ShouldReturnFruit_WhenExists()
        {
            // Arrange
            var expectedFruit = PomeFixture.Pear;
            var seedData = new FruitSeedData(expectedFruit);

            SetupInMemoryDb inMemoryDb = await SetupInMemoryDb.SeededAsync(seedData);
            var repository = inMemoryDb.CreateRepository();
            var service = new FruitService(repository, _mapper);            

            // Act
            var fruit = await service.FindFruitByIdAsync(expectedFruit.Id);

            // Assert
            Assert.That(fruit, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(fruit.Name, Is.EqualTo(expectedFruit.Name));
                Assert.That(fruit.Description, Is.EqualTo(expectedFruit.Description));
                Assert.That(fruit.FruitTypeId, Is.EqualTo(expectedFruit.FruitTypeId));
            });
        }

        [Test]
        public async Task SaveFruitAsync_ShouldAddFruit_WhenValidCreateFruitDTO()
        {
            // Arrange
            var expectedFruit = new SaveFruitRequest
            {
                Name = PomeFixture.Apple.Name,
                Description = PomeFixture.Apple.Description,
                FruitTypeId = PomeFixture.Apple.FruitTypeId
            };

            SetupInMemoryDb inMemoryDb = await SetupInMemoryDb.SeededAsync(new FruitSeedData());
            var repository = inMemoryDb.CreateRepository();
            var service = new FruitService(repository, _mapper);

            // Act
            var createdFruitId = (await service.SaveFruitAsync(expectedFruit)).Id;
            var createdFruit = await service.FindFruitByIdAsync(createdFruitId);

            // Assert
            Assert.That(createdFruit, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(createdFruit.Name, Is.EqualTo(expectedFruit.Name));
                Assert.That(createdFruit.Description, Is.EqualTo(expectedFruit.Description));
                Assert.That(createdFruit.FruitTypeId, Is.EqualTo(expectedFruit.FruitTypeId));
            });
        }

        [Test]
        public async Task UpdateFruit_ShouldUpdateFruit_WhenValidFruitDTO()
        {
            // Arrange
            var formerFruit = PomeFixture.Pear;
            var seedData = new FruitSeedData(formerFruit);

            SetupInMemoryDb inMemoryDb = await SetupInMemoryDb.SeededAsync(seedData);
            var repository = inMemoryDb.CreateRepository();
            var service = new FruitService(repository, _mapper);

            var expectedFruit = BerryFixture.Blueberry;

            var incomingFruitData = new UpdateFruitRequest
            {
                Id = formerFruit.Id,
                Name = expectedFruit.Name,
                Description = expectedFruit.Description,
                FruitTypeId = expectedFruit.FruitTypeId
            };

            // Act
            var updatedFruit = await service.UpdateFruitAsync(incomingFruitData);

            // Assert
            Assert.That(updatedFruit, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(updatedFruit.Name, Is.EqualTo(expectedFruit.Name));
                Assert.That(updatedFruit.Description, Is.EqualTo(expectedFruit.Description));
                Assert.That(updatedFruit.FruitTypeId, Is.EqualTo(expectedFruit.FruitTypeId));
            });
        }

        [Test]
        public async Task DeleteFruitAsync_SuccessfullyRemovesFruit_WhenFruitExists()
        {
            // Arrange
            var fruitToDelete = StoneFixture.Peach;
            var seedData = new FruitSeedData(fruitToDelete);

            SetupInMemoryDb inMemoryDb = await SetupInMemoryDb.SeededAsync(seedData);
            var repository = inMemoryDb.CreateRepository();
            var service = new FruitService(repository, _mapper);

            // Act & Assert
            var fruitsBeforeDelete = (await service.FindAllFruitsAsync()).Fruits.ToList();
            Assert.That(fruitsBeforeDelete, Has.Count.EqualTo(6));
            Assert.That(fruitsBeforeDelete, Has.Exactly(1).Matches<FruitDTO>(f => f.Name == fruitToDelete.Name));

            await service.DeleteFruitAsync(fruitToDelete.Id);

            var fruitsAfterDelete = (await service.FindAllFruitsAsync()).Fruits.ToList();

            Assert.Multiple(() =>
            {
                Assert.That(fruitsAfterDelete, Has.Count.EqualTo(5));
                Assert.That(fruitsAfterDelete, Has.Exactly(0).Matches<FruitDTO>(f => f.Id == fruitToDelete.Id));
            });
        }
    }
}