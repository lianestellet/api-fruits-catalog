using BusinessLogic.Services;
using BusinessLogic.UnitTests.Extensions;
using Entities.Domain;
using Entities.DTOs;
using Entities.Exceptions;
using Entities.Extensions;
using Entities.Interfaces;
using Entities.Validation;
using FluentValidation;
using Moq;
using NUnit.Framework.Constraints;
using TestUtils.Extensions;
using TestUtils.Fixtures;

namespace BusinessLogic.UnitTests.Services
{
    public class FruitServiceTests
    {
        private Mock<IFruitRepository> _mockRepository;
        private FruitDTOValidator _validator;
        private CreateFruitDTOValidator _createValidator;
        private FruitService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IFruitRepository>();
            _validator = new FruitDTOValidator();
            _createValidator = new CreateFruitDTOValidator();
            _service = new FruitService(_mockRepository.Object, _validator, _createValidator);
        }

        [Test]
        public async Task FindAllFruits_ReturnsAllFruits_WhenFruitsExist()
        {
            // Arrange
            List<Fruit> fruits = [CitrusFixture.Lemon, StoneFixture.Peach, TropicalFixture.Coconut];
            _mockRepository.StubFindAllFruitsAsync(fruits);

            // Act
            var result = await _service.FindAllFruits();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(fruits.Count));
        }

        [Test]
        public async Task FindFruitByIdAsync_ShouldReturnFruit()
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
        public async Task FindFruitByIdAsync_ThrowsNotFound_WhenFruitIdNotExist()
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
        public async Task SaveFruitAsync_ReturnsFruitDTO()
        {
            // Arrange
            var fruitDto = CitrusFixture.Lemon.ToCreateFruitDTO();
            var fruit = fruitDto.ToFruit();

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
        public async Task SaveFruitAsync_ThrowsValidationException_WhenFruitIsNotValid()
        {
            var invalidFruitDto = new CreateFruitDTO
            {
                Name = "",
                Description = "",
                FruitTypeId = 0,
            };

            try
            {
                await _service.SaveFruitAsync(invalidFruitDto);
                Assert.Fail("Expected ValidationException but non was thrown");
            }
            catch (ValidationException ex)
            {
                var errorMessages = ex.Errors.Select(e => e.ErrorMessage).ToList();
                Assert.That(errorMessages, Does.Contain(ValidationMessages.FruitNameRequired));
                Assert.That(errorMessages, Does.Contain(ValidationMessages.FruitDescriptionRequired));
                Assert.That(errorMessages, Does.Contain(ValidationMessages.FruitTypeIdRequired));
            }
        }

        [Test]
        public async Task SaveFruitAsync_ThrowsNotFoundException_WhenFruitTypeNotExist()
        {
            // Arrange
            var invalidFruitTypeId = -1;
            var fruitDto = new CreateFruitDTO 
            { 
                FruitTypeId = invalidFruitTypeId, 
                Name = "Kiwi",
                Description = "Tangy and useful for drinks"
            };

            var expectedExceptionMessage = ExceptionMessages.FruitTypeNotFoundById(invalidFruitTypeId);

            try
            {
                await _service.SaveFruitAsync(fruitDto);
                Assert.Fail("Expected NotFoundException but non was thrown");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedExceptionMessage));
            }
        }

        [Test]
        public async Task UpdateFruitAsync_ThrowsValidationException_WhenFruitDtoIsNotValid()
        {
            // Arrange
            var invalidFruitDto = new FruitDTO() { Name = "", Description = "" };            

            try
            {
                await _service.UpdateFruitAsync(invalidFruitDto);
                Assert.Fail("Expected ValidationException but non was thrown");
            }
            catch (ValidationException ex)
            {
                var errorMessages = ex.Errors.Select(e => e.ErrorMessage).ToList();
                Assert.That(errorMessages, Does.Contain(ValidationMessages.FruitNameRequired));
                Assert.That(errorMessages, Does.Contain(ValidationMessages.FruitDescriptionRequired));
                Assert.That(errorMessages, Does.Contain(ValidationMessages.FruitTypeIdRequired));
                Assert.That(errorMessages, Does.Contain(ValidationMessages.FruitIdRequired));
            }
        }

        [Test]
        public async Task UpdateFruitAsync_ThrowsNotFoundException_WhenFruitDoesNotExist()
        {
            // Arrange
            long nonExistentFruitId = -1;
            var fruitDto = PomeFixture.Pear.WithId(nonExistentFruitId).ToFruitDTO();

            var expectedExceptionMessage = ExceptionMessages.FruitNotFoundById(nonExistentFruitId);
            _mockRepository.StubFindFruitByIdAsyncAs(null);

            // Act & Assert
            var ex = await Task.Run(
                () => Assert.ThrowsAsync<NotFoundException>(
                    () => _service.UpdateFruitAsync(fruitDto)));

            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo(expectedExceptionMessage));
        }


        [Test]
        public async Task UpdateFruitAsync_ThrowsNotFoundException_WhenFruitTypeDoesNotExist()
        {
            // Arrange
            var invalidFruitTypeId = -1;
            var fruitDto = new FruitDTO
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
                await _service.UpdateFruitAsync(fruitDto);
                Assert.Fail("Expected NotFoundException but non was thrown");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedExceptionMessage));
            }
        }

        [Test]
        public async Task UpdateFruitAsync_ShouldUpdate_WhenFruitDtoIsValid()
        {
            // Arrange
            var updatedFruit = TropicalFixture.Papaya.WithId(1);
            var updateFruitDto = updatedFruit.ToFruitDTO();

            _mockRepository.StubFindFruitByIdAsyncAs(updatedFruit);
            _mockRepository.StubFindFruitTypeByIdAsyncAs(TropicalFixture.Type);
            _mockRepository.StubUpdateFruitAsyncAs(updatedFruit);

            var result = await _service.UpdateFruitAsync(updateFruitDto);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(updateFruitDto.Id));
                Assert.That(result.Name, Is.EqualTo(updateFruitDto.Name));
                Assert.That(result.Description, Is.EqualTo(updateFruitDto.Description));
                Assert.That(result.FruitTypeId, Is.EqualTo(updateFruitDto.FruitTypeId));
            });
        }

        [Test]
        public async Task DeleteFruitAsync_ThrowsNotFoundException_WhenFruitNotExist()
        {
            // Arrange
            var inexistentFruitId = -1;

            _mockRepository.StubFindFruitByIdAsyncAs(null);

            var expectedExceptionMessage = ExceptionMessages.FruitNotFoundById(inexistentFruitId);

            try
            {
                await _service.DeleteFruit(inexistentFruitId);
                Assert.Fail("Expected NotFoundException but non was thrown");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedExceptionMessage));
            }
        }

        [Test]
        public async Task DeleteFruitAsync_ShouldDelete_WhenFruitExists()
        {
            // Arrange
            var existingFruitId = 1;
            var existingFruit = PomeFixture.Apple;

            _mockRepository.StubFindFruitByIdAsyncAs(null);

            _mockRepository.Setup(r => r.FindFruitByIdAsync(existingFruitId)).ReturnsAsync(existingFruit); 
            _mockRepository.Setup(r => r.DeleteFruitAsync(existingFruit)).Returns(Task.CompletedTask);

            await _service.DeleteFruit(existingFruitId);            
        }
    }
}