﻿using Entities.Domain;
using Entities.Exceptions;
using TestUtils.Context;
using Utils.Data;
using Utils.Fixtures;

namespace DataAccess.UnitTests.Repositories
{
    [TestFixture]
    public class FruitRepositoryTests
    {
        [Test]
        public async Task FindAllFruitsAsync_ShouldReturnEmptyList_WhenNoFruitsExist()
        {
            // Arrange
            var dbContext = SetupInMemoryDb.Empty();
            var repository = dbContext.CreateRepository();

            // Act
            var fruits = (await repository.FindAllFruitsAsync()).ToList();

            // Assert
            Assert.That(fruits, Is.Empty);
        }

        [Test]
        public async Task FindAllFruits_ShouldReturnsAllFruits_WhenCalled()
        {
            // Arrange 
            var seedData = new SeedData().SeedBerriesFruits();
            var expectedFruits = seedData.SetFruits();
            var expectedFruitTypes = seedData.SetFruitTypes();

            // Act
            var dbContext = await SetupInMemoryDb.SeededAsync(seedData);
            var _repository = dbContext.CreateRepository();
            var allFruits = (await _repository.FindAllFruitsAsync()).ToList();

            Assert.That(allFruits, Is.Not.Empty);

            foreach (var actualFruit in allFruits)
            {
                var expectedFruit = expectedFruits.First(f => f.Id == actualFruit.Id);
                var expectedFruitType = expectedFruitTypes.First(ft => ft.Id == expectedFruit.FruitTypeId);

                Assert.Multiple(() =>
                {
                    Assert.That(actualFruit.Name, Is.EqualTo(expectedFruit.Name));
                    Assert.That(actualFruit.Description, Is.EqualTo(expectedFruit.Description));
                    Assert.That(actualFruit.FruitType!.Id, Is.EqualTo(expectedFruit.FruitTypeId));

                    // Verify the relationship with FruitTypeMessages
                    Assert.That(actualFruit.FruitType, Is.Not.Null);
                    Assert.That(actualFruit.FruitType.Name, Is.EqualTo(expectedFruitType.Name));
                    Assert.That(actualFruit.FruitType.Description, Is.EqualTo(expectedFruitType.Description));
                });
            }
        }

        [Test]
        public void FindByIdAsync_ShouldThrowException_WhenNotFound()
        {
            // Arrange
            var context = SetupInMemoryDb.Empty();
            var repository = context.CreateRepository();
            var invalidId = -1;


            // Assert
            var exception = Assert.ThrowsAsync<FruitNotFoundException>(async () => await repository.FindFruitByIdAsync(invalidId));
            Assert.That(exception.CustomMessage, Is.EqualTo(ExceptionMessages.FruitNotFoundById(invalidId)));
        }

        [Test]
        public async Task FindFruitByIdAsync_ShouldReturnFruit_WhenExists()
        {
            // Arrange
            FruitType expectedFruitType = StoneFixture.Type;
            Fruit expectedFruit = StoneFixture.Peach;

            var seedData = new SeedData()
                .SeedFruitType(expectedFruitType)
                .SeedFruit(expectedFruit);

            var dbContext = await SetupInMemoryDb.SeededAsync(seedData);
            var _repository = dbContext.CreateRepository();

            // Act
            var actualFruit = await _repository.FindFruitByIdAsync(expectedFruit.Id);

            // Assert
            Assert.That(actualFruit, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(actualFruit.Name, Is.EqualTo(expectedFruit.Name));
                Assert.That(actualFruit.Description, Is.EqualTo(expectedFruit.Description));
                Assert.That(actualFruit.FruitType!.Name, Is.EqualTo(expectedFruitType.Name));
            });
        }

        [Test]
        public async Task SaveFruitAsync_ShouldAddFruit_WhenValidData()
        {
            // Arrange 
            FruitType fruitType = new("Tropical", "Known for their exotic flavors and vibrant colors");
            Fruit fruit = new("Pineapple", "Tropical and tangy") { FruitTypeId = fruitType.Id };

            var seedData = new SeedData().SeedFruitType(fruitType);
            var _context = await SetupInMemoryDb.SeededAsync(seedData);
            var _repository = _context.CreateRepository();

            // Act
            var savedFruit = await _repository.SaveFruitAsync(fruit);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(savedFruit.Name, Is.EqualTo(fruit.Name));
                Assert.That(savedFruit.Description, Is.EqualTo(fruit.Description));
                Assert.That(savedFruit.FruitTypeId, Is.EqualTo(fruit.FruitTypeId));
            });
        }

        [Test]
        public async Task UpdateFruitAsync_ShouldAddFruit_WhenValid()
        {
            // Arrange
            FruitType expectedFruitType = TropicalFixture.Type;
            Fruit expectedFruit = TropicalFixture.Mango;

            FruitType fruitType = CitrusFixture.Type;
            Fruit fruit = CitrusFixture.Lemon;
            fruit.Id = 1;

            var seedData = new SeedData()
                .SeedFruit(fruit)
                .SeedFruitTypes([fruitType, expectedFruitType]);

            var _context = await SetupInMemoryDb.SeededAsync(seedData);
            var _repository = _context.CreateRepository();

            // Act
            fruit.Name = expectedFruit.Name;
            fruit.Description = expectedFruit.Description;
            fruit.FruitTypeId = expectedFruit.FruitTypeId;
            await _repository.UpdateFruitAsync(fruit);
            var updatedFruit = await _repository.FindFruitByIdAsync(fruit.Id);

            //Assert
            Assert.That(updatedFruit, Is.Not.Null);
            Assert.That(updatedFruit.FruitType, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(updatedFruit.Name, Is.EqualTo(expectedFruit.Name));
                Assert.That(updatedFruit.Description, Is.EqualTo(expectedFruit.Description));
                Assert.That(updatedFruit.FruitTypeId, Is.EqualTo(expectedFruitType.Id));
                Assert.That(updatedFruit.FruitType.Name, Is.EqualTo(expectedFruitType.Name));
                Assert.That(updatedFruit.FruitType.Description, Is.EqualTo(expectedFruitType.Description));
            });
        }

        [Test]
        public async Task DeleteFruitAsync_ShouldRemoveFruit_WhenExists()
        {
            // Arrange
            FruitType fruitType = new("Tropical")
            {
                Id = 1,
                Description = "Known for their exotic flavors and vibrant colors"
            };

            Fruit fruit = new("Pineapple")
            {
                Id = 2,
                Description = "Tropical and tangy",
                FruitTypeId = fruitType.Id
            };

            var seedData = new SeedData()
                .SeedFruitType(fruitType)
                .SeedFruit(fruit);

            var _context = await SetupInMemoryDb.SeededAsync(seedData);
            var repository = _context.CreateRepository();

            // Act
            await repository.DeleteFruitAsync(fruit.Id);

            var exception = Assert.ThrowsAsync<FruitNotFoundException>(async () => await repository.FindFruitByIdAsync(fruit.Id));
            Assert.That(exception.CustomMessage, Is.EqualTo(ExceptionMessages.FruitNotFoundById(fruit.Id)));
        }
    }
}
