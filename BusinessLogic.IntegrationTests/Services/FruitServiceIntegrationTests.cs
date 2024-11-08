using BusinessLogic.IntegrationTests.Extensions;
using BusinessLogic.Services;
using Entities.Domain;
using Entities.DTOs;
using Entities.Extensions;
using Entities.Validation;
using TestUtils.Core.Data;
using TestUtils.Core.Fixtures;
using TestUtils.DataAccess.Context;

namespace BusinessLogic.IntegrationTests.Services
{
    public class FruitServiceIntegrationTests
    {
        private FruitDTOValidator _validator;
        private CreateFruitDTOValidator _createValidator;

        [SetUp]
        public void Setup()
        {
            _validator = new FruitDTOValidator();
            _createValidator = new CreateFruitDTOValidator();
        }

        [Test]
        public async Task FindAllFruitsAsync_ShouldReturnAllFruits_WhenCalled()
        {
            // Arrange
            var expectedFruit = CitrusFixture.Lemon;
            var seedData = new FruitSeedData(expectedFruit);
            var expectedCount = seedData.Fruits.Count;

            SetupInMemoryDb inMemoryDb = await SetupInMemoryDb.SeededAsync(seedData);
            var _service = inMemoryDb.GetFruitService();

            // Act
            var fruits = (await _service.FindAllFruitsAsync()).ToList();

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
            var _service = inMemoryDb.GetFruitService();

            // Act
            var fruit = await _service.FindFruitByIdAsync(expectedFruit.Id);

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
            var expectedFruit = PomeFixture.Pear.ToCreateFruitDTO();

            SetupInMemoryDb inMemoryDb = await SetupInMemoryDb.SeededAsync(new FruitSeedData());
            var _service = inMemoryDb.GetFruitService();

            // Act
            var createdFruitId = (await _service.SaveFruitAsync(expectedFruit)).Id;
            var createdFruit = await _service.FindFruitByIdAsync(createdFruitId);

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
            var previousFruitData = PomeFixture.Pear;
            var seedData = new FruitSeedData(previousFruitData);

            SetupInMemoryDb inMemoryDb = await SetupInMemoryDb.SeededAsync(seedData);
            var _service = inMemoryDb.GetFruitService();

            var expectedFruitData = BerryFixture.Blueberry;
            previousFruitData.Name = expectedFruitData.Name;
            previousFruitData.Description = expectedFruitData.Description;
            previousFruitData.FruitTypeId = expectedFruitData.FruitTypeId;
                
            var incomingFruitData = previousFruitData.ToFruitDTO();

            // Act
            var updatedFruit = await _service.UpdateFruitAsync(incomingFruitData);

            // Assert
            Assert.That(updatedFruit, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(updatedFruit.Name, Is.EqualTo(incomingFruitData.Name));
                Assert.That(updatedFruit.Description, Is.EqualTo(incomingFruitData.Description));
                Assert.That(updatedFruit.FruitTypeId, Is.EqualTo(incomingFruitData.FruitTypeId));
            });
        }

        [Test]
        public async Task DeleteFruitAsync_SuccessfullyRemovesFruit_WhenFruitExists()
        {
            // Arrange
            var fruitToDelete = StoneFixture.Peach;
            var seedData = new FruitSeedData(fruitToDelete);

            SetupInMemoryDb inMemoryDb = await SetupInMemoryDb.SeededAsync(seedData);
            var _service = inMemoryDb.GetFruitService();

            // Act & Assert
            var fruitsBeforeDelete = (await _service.FindAllFruitsAsync()).ToList();
            Assert.That(fruitsBeforeDelete, Has.Count.EqualTo(6));
            Assert.That(fruitsBeforeDelete, Has.Exactly(1).Matches<FruitDTO>(f => f.Name == fruitToDelete.Name));

            await _service.DeleteFruitAsync(fruitToDelete.Id);

            var fruitsAfterDelete = (await _service.FindAllFruitsAsync()).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(fruitsAfterDelete, Has.Count.EqualTo(5));
                Assert.That(fruitsAfterDelete, Has.Exactly(0).Matches<FruitDTO>(f => f.Id == fruitToDelete.Id));
            });
        }
    }
}