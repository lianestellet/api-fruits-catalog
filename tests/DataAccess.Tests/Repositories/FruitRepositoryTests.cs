using DataAccess.Tests.Data;
using Entities.Domain;
using Entities.DTOs;

namespace DataAccess.Tests.Repositories
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
            var seedData = new DbSeedData();
            var expectedFruits = seedData.SetFruits();
            var expectedFruitTypes = seedData.SetFruitTypes();

            // Act
            var dbContext = await InMemoryDbContext.SeedDatabaseAsync(seedData);
            var _repository = dbContext.CreateRepository();
            var fruits = (await _repository.FindAllFruitsAsync()).ToList();

            Assert.That(fruits, Is.Not.Empty);

            for (int i = 0; i < expectedFruits.Count; i++)
            {
                var expectedFruit = expectedFruits[i];
                var actualFruit = fruits[i];
                var expectedFruitType = expectedFruitTypes.First(ft => ft.Id == expectedFruit.FruitTypeId);

                Assert.Multiple(() =>
                {
                    Assert.That(actualFruit.Name, Is.EqualTo(expectedFruit.Name));
                    Assert.That(actualFruit.Description, Is.EqualTo(expectedFruit.Description));
                    Assert.That(actualFruit.FruitType!.Id, Is.EqualTo(expectedFruit.FruitTypeId));

                    // Verify the relationship with FruitType
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
            FruitType fruitType = new("Citrus") { Description = "Sour and juicy fruits" };
            Fruit fruit = new("Peach")
            {
                Description = "Soft and fuzzy",
                FruitType = fruitType,
                FruitTypeId = fruitType.Id
            };

            // Act
            var seedData = new CustomDbSeedData([fruitType], [fruit]);
            var dbContext = await InMemoryDbContext.SeedDatabaseAsync(seedData);
            var _repository = dbContext.CreateRepository();
            var actualFruit = await _repository.FindFruitByIdAsync(fruit.Id);

            // Assert
            Assert.That(actualFruit, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(actualFruit.Name, Is.EqualTo("Peach"));
                Assert.That(actualFruit.Description, Is.EqualTo("Soft and fuzzy"));
                Assert.That(actualFruit.FruitType!.Name, Is.EqualTo("Citrus"));
            });
        }

        [Test]
        public async Task SaveFruitAsync_AddFruit()
        {
            // Arrange 
            FruitType fruitType = new("Tropical") { Description = "Known for their exotic flavors and vibrant colors" };
            Fruit fruit = new("Pineapple")
            {
                Description = "Tropical and tangy",
                FruitTypeId = fruitType.Id
            };

            // Act
            var seedData = new CustomDbSeedData([fruitType]);
            var _context = await InMemoryDbContext.SeedDatabaseAsync(seedData);
            var _repository = _context.CreateRepository();
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
            FruitType tropicalFruitType = new("Tropical") { 
                Id = 1,
                Description = "Known for their exotic flavors and vibrant colors" 
            };
            FruitType citricFruitType = new("Citric") { 
                Id = 2,
                Description = "Fruits rich in vitamin C with tangy flavors and bright colors"
            };
            Fruit fruit = new("Pineapple")
            {
                Description = "Tropical and tangy",
                FruitTypeId = tropicalFruitType.Id
            };

            var seedData = new CustomDbSeedData([tropicalFruitType, citricFruitType]);
            var _context = await InMemoryDbContext.SeedDatabaseAsync(seedData);
            var _repository = _context.CreateRepository();

            // Act
            fruit.Name = "Lime";
            fruit.Description = "Could be used in many ways on the foods and drinks";
            fruit.FruitTypeId = citricFruitType.Id;

            var updatedFruit = await _repository.UpdateFruitAsync(fruit);

            //Assert
            Assert.That(updatedFruit.FruitType, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(updatedFruit.Name, Is.EqualTo("Lime"));
                Assert.That(updatedFruit.Description, Is.EqualTo("Could be used in many ways on the foods and drinks"));
                Assert.That(updatedFruit.FruitTypeId, Is.EqualTo(citricFruitType.Id));
                Assert.That(updatedFruit.FruitType.Name, Is.EqualTo(citricFruitType.Name));
                Assert.That(updatedFruit.FruitType.Description, Is.EqualTo(citricFruitType.Description));
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

            Fruit fruit = new Fruit("Pineapple")
            {
                Description = "Tropical and tangy",                
                FruitTypeId = fruitType.Id
            };

            var seedData = new CustomDbSeedData([fruitType], [fruit]);
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
