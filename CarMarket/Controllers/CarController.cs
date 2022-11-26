using CarMarket.DAL;
using CarMarket.DAL.Interfaces;
using CarMarket.Domain.Models_Entity_;
using CarMarket.Domain.Response;
using CarMarket.Domain.ViewModels.Car;
using CarMarket.Service.Interface;
using Microsoft.AspNetCore.Authorization;
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
                return View(respons.Data.ToList());
            }
            return RedirectToAction("Error");
           
        }
        [HttpGet]
        public async Task<IActionResult> GetCar(int id)
        {
            var respons = await _carService.GetCar(id);

            if (respons.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(respons.Data);
            }
            return RedirectToAction("Error");
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var respons = await _carService.DeleteCar(id);

            if (respons.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(respons.Data);
            }
            return RedirectToAction("Error");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if(id == 0)
            {
                return View();
            }
            var respons = await _carService.GetCar(id);

            if (respons.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(respons.Data);
            }
            return RedirectToAction("Error");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Save(CarViewModel model)
        {
            ModelState.Remove("DateCreate");
            if (ModelState.IsValid)
            {
                if(model.Id == 0)
                {
                    byte[] imgData;
                    using(var BinaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imgData = BinaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    await _carService.CreateCar(model, imgData);
                }
                else
                {
                    await _carService.Edit(model.Id, model);
                }
                return RedirectToAction("GetCars");
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetTypes()
        {
            var types = _carService.GetTypes();
            return Json(types.Data);
        }
    }
}
