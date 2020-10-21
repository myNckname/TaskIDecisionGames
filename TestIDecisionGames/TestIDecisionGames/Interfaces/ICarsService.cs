using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestIDecisionGames.Models;

namespace TestIDecisionGames.Interfaces
{
    public interface ICarsService
    {
        List<Car> Get();
        Car Get(string Id);
        Car Update(Car car);
        void Remove(Car car);
        void Remove(string Id);
    }
}
