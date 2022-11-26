using CarMarket.DAL;
using CarMarket.DAL.Interfaces;
using CarMarket.Service.Interface;
using Microsoft.AspNetCore.Mvc;



namespace CarMarket.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var respons = await _carService.GetCars();
            if(respons.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(respons.Data);
            }
            return RedirectToAction("Error");
           
        }
    }
}
