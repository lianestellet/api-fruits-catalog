using DataAccess.UnitTests.Context;
using Entities.Domain;
using TestUtils.Data;
using TestUtils.Extensions;
using TestUtils.Fixtures;

namespace DataAccess.UnitTests.Repositories
{
    [TestFixture]
    public class FruitRepositoryTests
    {
        [Test]
        public async Task FindAllFruits_ReturnsEmptyList_WhenNoFruitsExist()
        {
            // Arrange
            var _repository = InMemoryDbContext.Empty().CreateRepository();

            // Act
            var fruits = (await _repository.FindAllFruitsAsync()).ToList();

            // Assert
            Assert.That(fruits, Is.Empty);
        }

        [Test]
        public async Task FindAllFruits_ReturnsAllFruits_WhenFruitsExists()
        {
            // Arrange 
            var seedData = new SeedData().SeedBerriesFruits();
            var expectedFruits = seedData.SetFruits();
            var expectedFruitTypes = seedData.SetFruitTypes();

            // Act
            var dbContext = await InMemoryDbContext.SeedDatabaseAsync(seedData);
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
        public async Task FindByIdAsync_ReturnsNull()
        {
            // Arrange
            var _repository = InMemoryDbContext.Empty().CreateRepository();

            // Act 
            var fruit = await _repository.FindFruitByIdAsync(-1);

            // Assert
            Assert.That(fruit, Is.Null);
        }

        [Test]
        public async Task FindFruitById_ReturnsFruit()
        {
            // Arrange
            FruitType expectedFruitType = StoneFixture.Type;
            Fruit expectedFruit = StoneFixture.Peach;

            var seedData = new SeedData()
                .SeedFruitType(expectedFruitType)
                .SeedFruit(expectedFruit);
            
            var dbContext = await InMemoryDbContext.SeedDatabaseAsync(seedData);
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
        public async Task SaveFruitAsync_AddFruit()
        {
            // Arrange 
            FruitType fruitType = new("Tropical", "Known for their exotic flavors and vibrant colors");
            Fruit fruit = new("Pineapple", "Tropical and tangy") { FruitTypeId = fruitType.Id };

            var seedData = new SeedData().SeedFruitType(fruitType);
            var _context = await InMemoryDbContext.SeedDatabaseAsync(seedData);
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
        public async Task UpdateFruitAsync_AddFruit()
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

            var _context = await InMemoryDbContext.SeedDatabaseAsync(seedData);
            var _repository = _context.CreateRepository();

            // Act            
            fruit.UpdateDetails(expectedFruit);
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
        public async Task DeleteFruitAsync_RemoveFruit()
        {
            // Arrange
            FruitType fruitType = new ("Tropical") 
            { 
                Id = 1, 
                Description = "Known for their exotic flavors and vibrant colors" 
            };

            Fruit fruit = new ("Pineapple")
            {
                Description = "Tropical and tangy",
                FruitTypeId = fruitType.Id
            };

            var seedData = new SeedData()
                .SeedFruitType(fruitType)
                .SeedFruit(fruit);

            var _context = await InMemoryDbContext.SeedDatabaseAsync(seedData);
            var _repository = _context.CreateRepository();

            // Act            
            var fruitToDelete = await _repository.FindFruitByIdAsync(fruit.Id);

            Assert.That(fruitToDelete, Is.Not.Null);

            await _repository.DeleteFruitAsync(fruitToDelete);
            var fruitInDb = await _repository.FindFruitByIdAsync(fruit.Id);

            // Assert            
            Assert.That(fruitInDb, Is.Null);
        }
    }
}
