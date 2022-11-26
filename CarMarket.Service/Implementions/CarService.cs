
using CarMarket.DAL.Interfaces;
using CarMarket.Domain.Enum;
using CarMarket.Domain.Models_Entity_;
using CarMarket.Domain.Response;
using CarMarket.Service.Interface;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace CarMarket.Service.Implementions
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
        {
            var baseResponse = new BaseResponse<IEnumerable<Car>>();
            try
            {
                var cars = await _carRepository.Select();
                return DataVerification<IEnumerable<Car>>(cars);
            }
            catch(Exception ex) 
            {
                return new BaseResponse<IEnumerable<Car>>()
                {
                    Description = $"[GetCars] : {ex.Message}",
                    StatusCode = StatusCode.IntrenalServerError
                };
            }
        }

        public async Task<IBaseResponse<Car>> GetCar(int id)
        {
            
            try
            {
                var car = await _carRepository.Get(id);
                return DataVerification<Car>(car);
            }
            catch(Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    StatusCode = StatusCode.IntrenalServerError
                };
            }
        }

        public async Task<IBaseResponse<Car>> GetCarByName(string name)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var car = await _carRepository.GetByName(name);
                return DataVerification<Car>(car);
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    StatusCode = StatusCode.IntrenalServerError
                };
            }
        }


       private IBaseResponse<T> DataVerification<T>(T data)
        {
            var baseResponse = new BaseResponse<T>();
            if (data == null)
            {
                baseResponse.Description = "Car not found";
                baseResponse.StatusCode = StatusCode.CarNotFound;
                return baseResponse;
            }
            baseResponse.Data = data;
            baseResponse.StatusCode = StatusCode.OK;
            return baseResponse;
        }

    }
}
