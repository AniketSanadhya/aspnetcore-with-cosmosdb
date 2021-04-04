using DemoCarApp.Models;
using DemoCarApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoCarApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : Controller
    {
        private readonly ICarsService carsService;

        public CarsController(ICarsService carsService)
        {
            this.carsService = carsService;
        }

        [HttpGet]
        public async Task<List<Cars>> Get()
        {
            return await carsService.GetCars();
        }

        [HttpPost]
        public async Task Add(Cars car)
        {
             await carsService.Add(car);
        }

        [HttpPut]
        public async Task Update(Guid id)
        {
            await carsService.Update(id);
        }

        [HttpDelete]
        public async Task Delete(Guid id)
        {
            await carsService.Delete(id);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<Cars> GetById(Guid id)
        {
            return await carsService.GetCarById(id);
        }
    }
}
