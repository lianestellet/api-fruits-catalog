using API.Models.Request;
using AutoMapper;
using BusinessLogic.Mappings;
using BusinessLogic.Models.Request;
using BusinessLogic.Services;
using BusinessLogic.UnitTests.Extensions;
using BusinessLogic.UnitTests.Mappings;
using DataAccess.Repositories;
using Entities.Domain;
using Entities.Exceptions;
using Moq;
using Utils.Fixtures;

namespace BusinessLogic.UnitTests.Services
{
    public class FruitServiceUnitTests
    {
        private Mock<IFruitRepository> _mockRepository;
        private FruitService _service;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IFruitRepository>();
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile<BusinessLogicMappingProfile>(); mc.AddProfile<TestMappingProfile>(); });
            _mapper = mappingConfig.CreateMapper();
            _service = new FruitService(_mockRepository.Object, _mapper);
        }

        [Test]
        public async Task FindAllFruitsAsync_ShouldReturnAllFruits()
        {
            // Arrange
            List<Fruit> fruits = [CitrusFixture.Lemon, StoneFixture.Peach, TropicalFixture.Coconut];
            _mockRepository.StubFindAllFruitsAsync(fruits);

            // Act
            var result = await _service.FindAllFruitsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Fruits, Has.Count.EqualTo(fruits.Count));
        }

        [Test]
        public async Task FindFruitByIdAsync_ShouldReturnFruit_WhenExists()
        {
            // Arrange
            var fruit = CitrusFixture.Lemon;
            _mockRepository.StubFindFruitByIdAsyncAs(fruit);

            // Act
            var result = await _service.FindFruitByIdAsync(fruit.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo(fruit.Name));
                Assert.That(result.Description, Is.EqualTo(fruit.Description));
            });
        }

        [Test]
        public async Task FindFruitByIdAsync_ThrowsNotFoundException_WhenFruitNotExist()
        {
            // Arrange
            var nonExistentFruitId = -1;
            var expectedExceptionMessage = ExceptionMessages.FruitNotFoundById(nonExistentFruitId);
            _mockRepository.StubFindFruitByIdAsyncAs(null);

            // Act & Assert
            var ex = await Task.Run(
                () => Assert.ThrowsAsync<NotFoundException>(
                    () => _service.FindFruitByIdAsync(nonExistentFruitId)));

            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo(expectedExceptionMessage));
        }


        [Test]
        public async Task SaveFruitAsync_ShouldReturnFruitDTO_WhenValidCreateFruitDTO()
        {
            // Arrange
            var fruitDto = _mapper.Map<SaveFruitRequest>(CitrusFixture.Lemon);
            var fruit = _mapper.Map<Fruit>(fruitDto);

            _mockRepository.StubFindFruitTypeByIdAsyncAs(CitrusFixture.Type);                
            _mockRepository.StubSaveFruitAsyncAs(fruit);

            // Act
            var result = await _service.SaveFruitAsync(fruitDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo(fruitDto.Name));
                Assert.That(result.Description, Is.EqualTo(fruitDto.Description));
                Assert.That(result.FruitTypeId, Is.EqualTo(fruitDto.FruitTypeId));
            });
        }

        [Test]
        public void SaveFruitAsync_ThrowsNotFoundException_WhenFruitTypeNotExist()
        {
            // Arrange
            var invalidFruitTypeId = -1;
            var fruitDto = new SaveFruitRequest 
            { 
                FruitTypeId = invalidFruitTypeId, 
                Name = "Kiwi",
                Description = "Tangy and useful for drinks"
            };

            var expectedExceptionMessage = ExceptionMessages.FruitTypeNotFoundById(invalidFruitTypeId);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _service.SaveFruitAsync(fruitDto));
            Assert.That(exception.Message, Is.EqualTo(ExceptionMessages.FruitTypeNotFoundById(invalidFruitTypeId)));
        }

        [Test]
        public async Task UpdateFruitAsync_ThrowsNotFoundException_WhenFruitNotExist()
        {
            // Arrange
            long nonExistentFruitId = -1;
            var expectedExceptionMessage = ExceptionMessages.FruitNotFoundById(nonExistentFruitId);
            
            var updateFruitDto = _mapper.Map<UpdateFruitRequest>(PomeFixture.Pear);
            updateFruitDto.Id = nonExistentFruitId;

            _mockRepository.StubFindFruitTypeByIdAsyncAs(PomeFixture.Type);
            _mockRepository.StubUpdateFruitAsyncFruitNotFoundException(updateFruitDto);

            // Act & Assert
            var ex = await Task.Run(
                () => Assert.ThrowsAsync<NotFoundException>(
                    () => _service.UpdateFruitAsync(updateFruitDto)));

            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo(expectedExceptionMessage));
        }


        [Test]
        public async Task UpdateFruitAsync_ThrowsNotFoundException_WhenFruitTypeNotExist()
        {
            // Arrange
            var invalidFruitTypeId = -1;
            var updateFruitDTO = new UpdateFruitRequest
            {
                Id = 1,
                FruitTypeId = invalidFruitTypeId,
                Name = "Kiwi",
                Description = "Tangy and useful for drinks"
            };


            _mockRepository.StubFindFruitByIdAsyncAs(new Fruit());
            _mockRepository.StubFindFruitTypeByIdAsyncAs(null);

            var expectedExceptionMessage = ExceptionMessages.FruitTypeNotFoundById(invalidFruitTypeId);

            try
            {
                await _service.UpdateFruitAsync(updateFruitDTO);
                Assert.Fail("Expected NotFoundException but non was thrown");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedExceptionMessage));
            }
        }

        [Test]
        public async Task UpdateFruitAsync_ShouldUpdateFruit_WhenValidFruitDTO()
        {
            // Arrange
            var fruit = TropicalFixture.Papaya;
            var fruitType = TropicalFixture.Type;

            var updatedFruit = CitrusFixture.Lemon;
            var updatedFruitType = CitrusFixture.Type;

            var expectedFruitDto = _mapper.Map<UpdateFruitRequest>(fruit);

            _mockRepository.StubFindFruitByIdAsyncAs(fruit);
            _mockRepository.StubFindFruitTypeByIdAsyncAs(TropicalFixture.Type);



            _mockRepository.StubUpdateFruitAsyncAs(fruit);
            var result = await _service.UpdateFruitAsync(expectedFruitDto);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(expectedFruitDto.Id));
                Assert.That(result.Name, Is.EqualTo(expectedFruitDto.Name));
                Assert.That(result.Description, Is.EqualTo(expectedFruitDto.Description));
                Assert.That(result.FruitTypeId, Is.EqualTo(expectedFruitDto.FruitTypeId));
            });
        }

        [Test]
        public async Task DeleteFruitAsync_ThrowsNotFoundException_WhenFruitDoesNotExist()
        {
            // Arrange
            var inexistentFruitId = -1;

            _mockRepository.StubFindFruitByIdAsyncAs(null);

            var expectedExceptionMessage = ExceptionMessages.FruitNotFoundById(inexistentFruitId);

            try
            {
                await _service.DeleteFruitAsync(inexistentFruitId);
                Assert.Fail("Expected NotFoundException but non was thrown");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedExceptionMessage));
            }
        }

        [Test]
        public async Task DeleteFruitAsync_ShouldDeleteFruit_WhenExists()
        {
            // Arrange
            var existingFruitId = 1;
            var existingFruit = PomeFixture.Apple;

            _mockRepository.StubFindFruitByIdAsyncAs(null);

            _mockRepository.Setup(r => r.FindFruitByIdAsync(existingFruitId)).ReturnsAsync(existingFruit);
            _mockRepository.Setup(r => r.DeleteFruitAsync(existingFruitId)).Returns(Task.CompletedTask);

            await _service.DeleteFruitAsync(existingFruitId);
        }
    }
}