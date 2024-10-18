using DataAccess.Context;
using DataAccess.Repositories;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Tests
{
    [TestFixture]
    public class FruitRepositoryTests
    {
        private DbContextOptions<FruitDbContext> _dbContextOptions;
        private FruitRepository _repository;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<FruitDbContext>()
                                .UseInMemoryDatabase("EmployeeTestDb")
                                .Options;
            _repository = new FruitRepository(new FruitDbContext(_dbContextOptions));
        }

        [Test]
        public async Task FindAllFruits_ReturnsEmptyList_WhenNoFruitsExist()
        {
            // Act
            var fruits = (await _repository.FindAllFruits()).ToList();

            // Assert
            Assert.That(fruits.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task FindAllFruits_ReturnsAllFruits_WhenFruitsExists()
        {
            var fruitTypeDTO = new FruitTypeDTO { Name = "Citrus", Description = "they have acid in composition" };
            var fruitDTO = new FruitDTO { Id = 1, Name = "lemon", Description = "it's a bit citric", FruitType = fruitTypeDTO };

            await _repository.Save(fruitDTO);

            var fruits = (await _repository.FindAllFruits()).ToList();
            Assert.That(fruits.Count, Is.EqualTo(1));
            Assert.That(fruits.First().Name, Is.EqualTo("John Doe"));
        }

        //[Test]
        //public async Task CanRetrieveEmployeeById()
        //{
        //    //var employee = new Employee { Id = 1, Name = "Jane Doe", Description = "Another sample employee" };
        //    //await _repository.Save(employee);

        //    //var retrievedEmployee = await _repository.FindById(1);
        //    //Assert.IsNotNull(retrievedEmployee);
        //    //Assert.That(retrievedEmployee.Name, Is.EqualTo("Jane Doe"));
        //}

    }
}
