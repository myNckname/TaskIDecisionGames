using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

using TestIDecisionGames.Interfaces;
using TestIDecisionGames.Models;

namespace TestIDecisionGames.Repositories
{
    public class MongoCarsRepository : ICarsRepository
    {
        private readonly IMongoCollection<Car> _cars;

        public MongoCarsRepository(ICarsDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cars = database.GetCollection<Car>("Cars");
        }
        public IEnumerable<Car> Get()
        {
            return _cars.Find(car => true).ToList();
        }

        public Car Get(string id)
        {
            return _cars.Find(car => car.Id == id).FirstOrDefault();
        }

        public Car Create(Car car)
        {
            car.Id = Guid.NewGuid().ToString();
            _cars.InsertOne(car);
            return Get(car.Id);
        }
        public Car Update(Car car)
        {
            var filter = Builders<Car>.Filter.Eq("Id", car.Id);
            _cars.ReplaceOne(filter, car);
            return Get(car.Id);
        }
        public void Remove(string id)
        {
            _cars.DeleteOne(car => car.Id == id);
        }

    }
}
