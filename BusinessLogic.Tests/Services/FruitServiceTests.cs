using BusinessLogic.Services;
using Entities.DTOs;
using Entities.Interfaces;
using Moq;

namespace BusinessLogic.Tests.Services
{
    public class FruitServiceTests
    {
        private Mock<IFruitRepository> _mockFruitRepository;
        private FruitService _fruitService;

        [SetUp]
        public void Setup()
        {
            _mockFruitRepository = new Mock<IFruitRepository>();
            _fruitService = new FruitService(_mockFruitRepository.Object);
        }

        [Test]
        public void FindAllFruits_ReturnsAllFruits_WhenFruitsExist()
        {
            // Arrange
            //var fruits = new List<FruitDTO>
            //Assert.Pass();
        }
    }
}