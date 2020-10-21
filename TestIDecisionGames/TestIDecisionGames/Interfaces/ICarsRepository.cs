using System.Collections.Generic;

using TestIDecisionGames.Models;

namespace TestIDecisionGames.Interfaces
{
    public interface ICarsRepository
    {
        IEnumerable<Car> Get();
        Car Get(string id);
        Car Create(Car car);
        Car Update(Car car);
        void Remove(string Id);
    }
}
