using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestIDecisionGames.Interfaces;
using TestIDecisionGames.Models;

namespace TestIDecisionGames.Services
{
    public class CarsService : ICarsService
    {
        private readonly ICarsRepository _carsRepository;
        public CarsService(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        public List<Car> Get()
        {
            return _carsRepository.Get().ToList();
        }
        public Car Get(string Id)
        {
            return _carsRepository.Get(Id);
        }
        public Car Update(Car car)
        {
            var existingCar = Get(car.Id);

            if (existingCar == null)
            {
                return _carsRepository.Create(car);
            }

            var newCarProperties = car.GetType().GetProperties();
            foreach (PropertyInfo property in newCarProperties)
            {
                var value = property.GetValue(car);
                if (value != null)
                {
                    property.SetValue(existingCar, value);
                }
            }
            return _carsRepository.Update(existingCar);
        }
        public void Remove(Car car)
        {
            _carsRepository.Remove(car.Id);
        }
        public void Remove(string Id)
        {
            _carsRepository.Remove(Id);
        }
    }
}
