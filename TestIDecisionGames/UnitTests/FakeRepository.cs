using System;
using System.Collections.Generic;
using TestIDecisionGames.Interfaces;
using TestIDecisionGames.Models;

namespace UnitTests
{
    class FakeRepository : ICarsRepository
    {
        private readonly List<Car> _cars = new List<Car> {
            new Car { Id = "1", Description = "some desc1", Name = "some name1" },
            new Car { Id = "2", Description = "some desc2", Name = "some name2" },
            new Car { Id = "3", Description = "some desc3", Name = "some name3" },
            new Car { Id = "4", Description = "some desc4", Name = "some name4" }
        };


        public IEnumerable<Car> Get()
        {
            return _cars;
        }

        public Car Get(string id)
        {
            return _cars.Find(car => car.Id == id);
        }

        public Car Create(Car car)
        {
            car.Id = $"{_cars.Count+1}";
            _cars.Add(car);
            return car;
        }
        public Car Update(Car car)
        {
            Remove(car.Id);
            _cars.Add(car);
            return car;
        }
        public void Remove(string id)
        {
            _cars.RemoveAll(car => car.Id == id);
        }

    }
}