using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using TestIDecisionGames;
using TestIDecisionGames.Interfaces;
using TestIDecisionGames.Models;
using System.Text;
using System.Collections.Generic;

namespace UnitTests.Controller
{
    public class CarsControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public CarsControllerTests()
        {
            _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>()
            .ConfigureTestServices(services =>
            {
                services.AddSingleton<ICarsRepository, FakeRepository>();
            }));

            _client = _server.CreateClient();
        }


        [Fact]
        public async Task Create_ReturnsCreatedCar()
        {
            var car = new Car { Id = "1", Description = "desc", Name = "name" };

            var json = JsonConvert.SerializeObject(car);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/cars", data);
            var resposneString = await response.Content.ReadAsStringAsync();

            Assert.Equal(car.Id, JsonConvert.DeserializeObject<Car>(resposneString).Id);
            Assert.Equal(car.Name, JsonConvert.DeserializeObject<Car>(resposneString).Name);
            Assert.Equal(car.Description, JsonConvert.DeserializeObject<Car>(resposneString).Description);
        }

        [Fact]

        public async Task Update_ReturnsUpdatedCar()
        {
            var car = new Car { Id = "1", Name = "new name" };
            var updatedCar = new Car { Id = "1", Description = "some desc1", Name = "new name" };

            var json = JsonConvert.SerializeObject(car);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/cars", data);
            var resposneString = await response.Content.ReadAsStringAsync();

            Assert.Equal(updatedCar.Id, JsonConvert.DeserializeObject<Car>(resposneString).Id);
            Assert.Equal(updatedCar.Name, JsonConvert.DeserializeObject<Car>(resposneString).Name);
            Assert.Equal(updatedCar.Description, JsonConvert.DeserializeObject<Car>(resposneString).Description);
        }
        [Fact]

        public async Task Update_BodyWithoutId_ReturnsNewCreatedCar()
        {
            var car = new Car { Name = "new name" };
            var createdCar = new Car { Id = "5", Name = "new name" };

            var json = JsonConvert.SerializeObject(car);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/cars", data);
            var resposneString = await response.Content.ReadAsStringAsync();

            Assert.Equal(createdCar.Id, JsonConvert.DeserializeObject<Car>(resposneString).Id);
            Assert.Equal(createdCar.Name, JsonConvert.DeserializeObject<Car>(resposneString).Name);
            Assert.Equal(createdCar.Description, JsonConvert.DeserializeObject<Car>(resposneString).Description);
        }

        [Fact]
        public async Task Get_ReturnsAllCars()
        {
            var response = await _client.GetAsync("api/cars");
            var resposneString = await response.Content.ReadAsStringAsync();

            Assert.Equal(4,JsonConvert.DeserializeObject<List<Car>>(resposneString).Count);
        }
    }
}
