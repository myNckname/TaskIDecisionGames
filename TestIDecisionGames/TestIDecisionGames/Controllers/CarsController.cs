using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TestIDecisionGames.Interfaces;
using TestIDecisionGames.Models;

namespace TestIDecisionGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarsService _carsService;
        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        [HttpGet]
        public ActionResult<List<Car>> Get()
        {
            return _carsService.Get();
        }
        [HttpGet("{id}")]
        public ActionResult<Car> Get(string id)
        {
            var car = _carsService.Get(id);
            if (car != null)
            {
                return car;
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Car> Update(Car car)
        {
            return _carsService.Update(car);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var car = _carsService.Get(id);

            if (car != null)
            {
                _carsService.Remove(car.Id);
                return Ok();
            }
            return NotFound();
        }
    }
}