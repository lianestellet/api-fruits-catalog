using API.Controllers;
using API.Models.Request;
using AutoMapper;
using BusinessLogic.Mappings;
using BusinessLogic.Models.DTOs;
using BusinessLogic.Models.Request;
using BusinessLogic.Models.Response;
using BusinessLogic.Services;
using Entities.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Utils.Fixtures;

namespace API.UnitTests.Controllers
{
    public class Tests
    {
        private Mock<IFruitService> _mockService;
        private FruitController _controller;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IFruitService>();            
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile<BusinessLogicMappingProfile>(); });
            _mapper = mappingConfig.CreateMapper();
            _controller = new FruitController(_mockService.Object);
        }

        [Test]
        public async Task FindAllFruits_ShouldReturnAllFruits()
        {
            // Arrange
            var fruitList = new List<Fruit> { CitrusFixture.Lemon, StoneFixture.Peach };
            List<FruitDTO> fruitDTOs = _mapper.Map<List<FruitDTO>>(fruitList);
            var fruitListResponse = new FruitListResponse(fruitDTOs);

            _mockService.Setup(service => service.FindAllFruitsAsync()).ReturnsAsync(fruitListResponse);

            // Act
            var result = await _controller.FindAllFruits() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var response = result.Value as FruitListResponse;
            Assert.That(response, Is.Not.Null);

            var fruits = response.Fruits;            
            Assert.Multiple(() =>            
            {
                Assert.That(fruits, Has.Count.EqualTo(2));
                Assert.That(fruits[0].Name, Is.EqualTo(CitrusFixture.Lemon.Name));
                Assert.That(fruits[1].Name, Is.EqualTo(StoneFixture.Peach.Name));
            });
        }

        [Test]
        public async Task FindFruitById_ShouldReturnFruit()
        {
            // Arrange
            var fruit = TropicalFixture.Coconut;
            FruitDTO fruitDTO = _mapper.Map<FruitDTO>(fruit);
            var fruitResponse = new FruitResponse(fruitDTO);

            _mockService.Setup(service => service.FindFruitByIdAsync(fruit.Id)).ReturnsAsync(fruitResponse);

            // Act
            var result = await _controller.FindFruitById(fruit.Id) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var response = result.Value as FruitResponse;
            Assert.That(response, Is.Not.Null);
            
            Assert.Multiple(() =>
            {
                Assert.That(response.Name, Is.EqualTo(fruit.Name));
                Assert.That(response.Description, Is.EqualTo(fruit.Description));                
            });
        }

        [Test]
        public async Task SaveFruit_ShouldReturnFruit()
        {
            // Arrange

            var createFruit = new SaveFruitRequest
            {
                Name = "Tomato",
                Description = "Don't seem like a fruit",
                FruitTypeId = 1,
            };

            var FruitDto = new FruitDTO
            {
                Id = 1,
                Name = createFruit.Name,
                Description = createFruit.Description,
                FruitTypeId = createFruit.FruitTypeId,
                FruitTypeName = "something",
                FruitTypeDescription = "doesn't feel like a fruit"
            };

            var fruitResponse = new FruitResponse(FruitDto);

            _mockService.Setup(service => service.SaveFruitAsync(createFruit)).ReturnsAsync(fruitResponse);

            // Act
            var result = await _controller.SaveFruit(createFruit) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var response = result.Value as FruitResponse;
            Assert.That(response, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(response.Name, Is.EqualTo("Tomato"));
                Assert.That(response.Description, Is.EqualTo("Don't seem like a fruit"));
                Assert.That(response.FruitTypeId, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task UpdateFruit_ShouldReturnFruit()
        {
            // Arrange
            var fruit = TropicalFixture.Coconut;

            FruitDTO fruitDTO = _mapper.Map<FruitDTO>(fruit);
            var fruitUpdated = new FruitDTO
            {
                Id = fruit.Id,
                Name = "Papaya",
                FruitTypeId = 3,
                Description = "some great description"
            };

            var updateRequest = new UpdateFruitRequest
            {
                Id = fruit.Id,
                Name = fruit.Name,
                Description = fruit.Description,
                FruitTypeId = fruit.FruitTypeId
            };

            var fruitResponse = new FruitResponse(fruitUpdated);
            var updateDTO = new UpdateFruitRequest
            {
                Id = updateRequest.Id,
                Name = updateRequest.Name,
                Description = updateRequest.Description,
                FruitTypeId = updateRequest.FruitTypeId,
            };

            _mockService.Setup(service => service.UpdateFruitAsync(updateRequest)).ReturnsAsync(fruitResponse);

            // Act
            var result = await _controller.UpdateFruit(updateRequest.Id, updateRequest) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var response = result.Value as FruitResponse;
            Assert.That(response, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(response.Name, Is.EqualTo("Papaya"));
                Assert.That(response.Description, Is.EqualTo("some great description"));
                Assert.That(response.FruitTypeId, Is.EqualTo(3));
            });
        }


        [Test]
        public async Task DeleteFruit_ShouldDeleteFruit()
        {
            // Arrange
            var fruitId = 2;
            var existingFruit = new FruitDTO
            {
                Id = fruitId,
                Name = "fruit",
                Description = "Fruit description",
                FruitTypeId = 20
            };

            var fruitResponse = new FruitResponse(existingFruit);

            _mockService.Setup(service => service.FindFruitByIdAsync(fruitId)).ReturnsAsync(fruitResponse);
            _mockService.Setup(service => service.DeleteFruitAsync(fruitId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteFruit(fruitId) as NoContentResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(204));
        }
    }
}