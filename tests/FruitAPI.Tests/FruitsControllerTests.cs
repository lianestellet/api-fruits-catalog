using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Entities;
using Tests;

namespace FruitApplication.FruitTest
{
    [TestFixture]
    public class FruitsControllerTests
    {
        private WebApiFactory _factory;
        private HttpClient _client;
        [SetUp]
        public void SetUp()
        {
            _factory = new WebApiFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task CreateFruitRespondsWithCreated()
        {
            var content = CreateContentFruit("Papaya", "Tropical", CreateFruitType());
            var response = await _client.PostAsync("/fruits", content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            string responseBody = await response.Content.ReadAsStringAsync();

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var actual = JsonSerializer.Deserialize<FruitDTO>(responseBody, options);
                Assert.That(actual.Id, Is.EqualTo(1));
                Assert.That(actual.Name, Is.EqualTo("Papaya"));
                Assert.That(actual.Description, Is.EqualTo("Tropical"));
            }
            catch (JsonException)
            {
                Assert.Fail("Could not deserialize response JSON:" + Truncate(responseBody));
            }
        }


        [Test]
        public async Task GetFruitsRespondsWithOk()
        {
            var content = CreateContentFruit("Melon", "Delicious", CreateFruitType());
            var response = await _client.PostAsync("/fruits", content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            response = await _client.GetAsync("/fruits");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            string responseBody = await response.Content.ReadAsStringAsync();

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var actual = JsonSerializer.Deserialize<List<FruitDTO>>(responseBody, options);
                Assert.That(actual.Count, Is.EqualTo(1));
                Assert.That(actual[0].Id, Is.EqualTo(1));
                Assert.That(actual[0].Name, Is.EqualTo("Melon"));
                Assert.That(actual[0].Description, Is.EqualTo("Delicious"));
            }
            catch (JsonException)
            {
                Assert.Fail("Could not deserialize response JSON:" + Truncate(responseBody));
            }
        }


        [Test]
        public async Task GetFruitRespondsWithOk()
        {
            var content = CreateContentFruit("Melon", "Delicious", CreateFruitType());
            var response = await _client.PostAsync("/fruits", content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            response = await _client.GetAsync($"/fruits/1");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            string responseBody = await response.Content.ReadAsStringAsync();

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var actual = JsonSerializer.Deserialize<FruitDTO>(responseBody, options);
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.Id, Is.EqualTo(1));
                Assert.That(actual.Name, Is.EqualTo("Melon"));
                Assert.That(actual.Description, Is.EqualTo("Delicious"));
            }
            catch (JsonException)
            {
                Assert.Fail("Could not deserialize response JSON:" + Truncate(responseBody));
            }
        }

        [Test]
        public async Task DeleteFruitRespondsWithNotContent()
        {
            var content = CreateContentFruit("Banana", "Yellow", CreateFruitType());
            var response = await _client.PostAsync("/fruits", content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            response = await _client.DeleteAsync("/fruits/1");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            response = await _client.GetAsync("/fruits");
            string responseBody = await response.Content.ReadAsStringAsync();

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var actual = JsonSerializer.Deserialize<List<FruitDTO>>(responseBody, options);
                Assert.That(actual.Count, Is.EqualTo(0));
            }
            catch (JsonException)
            {
                Assert.Fail("Could not deserialize response JSON:" + Truncate(responseBody));
            }
        }
        [Test]
        public async Task DeleteFruitRespondsWithNotFound()
        {
            var response = await _client.DeleteAsync("/fruits/10");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task PutFruitRespondsWithNoContent()
        {
            var content = CreateContentFruit("Orange", "From California", CreateFruitType());
            var response = await _client.PostAsync("/fruits", content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            content = CreateContentFruit(1, "Orange", "From California State", CreateFruitType());
            response = await _client.PutAsync("/fruits/1", content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            response = await _client.GetAsync("/fruits/1");
            string responseBody = await response.Content.ReadAsStringAsync();

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var actual = JsonSerializer.Deserialize<FruitDTO>(responseBody, options);
                Assert.That(actual.Id, Is.EqualTo(1));
                Assert.That(actual.Name, Is.EqualTo("Orange"));
                Assert.That(actual.Description, Is.EqualTo("From California State"));
            }
            catch (JsonException)
            {
                Assert.Fail("Could not deserialize response JSON:" + Truncate(responseBody));
            }
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        private static ByteArrayContent CreateContentFruit(string name, string description, FruitTypeDTO fruitType)
        {
            var item = JsonSerializer.Serialize(
                new { Name = name, Description = description, Type = fruitType }
            );
            var content = new StringContent(item);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
        private static ByteArrayContent CreateContentFruit(long id, string name, string description, FruitTypeDTO fruitType)
        {
            var item = JsonSerializer.Serialize(
                new { Id = id, Name = name, Description = description, Type = fruitType }
            );
            var content = new StringContent(item);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        private static FruitTypeDTO CreateFruitType()
        {
            return new FruitTypeDTO
            {
                Id = 1,
                Name = "Citric",
                Description = "Like oranges",
            };
        }

        private static string Truncate(string s, int threshold = 200, int trunc = 50)
        {
            if (s.Length > threshold)
            {
                return s.Substring(0, trunc) + "..." + s.Substring(s.Length - trunc);
            }

            return s;
        }
    }
}