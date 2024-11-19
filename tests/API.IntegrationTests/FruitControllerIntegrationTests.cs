using BusinessLogic.Models.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace API.IntegrationTests
{
    [TestFixture]
    public class FruitControllerIntegrationTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory<Startup> _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new CustomWebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:5001");
        }

        [TearDown]
        public void TearDown() 
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task GetAllFruits_ReturnsSuccess()
        {
            // Act
            var response = await _client.GetAsync("api/fruit");

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<FruitListResponse>();
            Assert.That(result, Is.Not.Null);

            var fruits = result.Fruits;
            Assert.That(fruits, Has.Count.EqualTo(1));
        }
    }
}
