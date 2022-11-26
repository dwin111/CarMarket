
using CarMarket.DAL.Interfaces;
using CarMarket.Domain.Enum;
using CarMarket.Domain.Models_Entity_;
using CarMarket.Domain.Response;
using CarMarket.Service.Interface;
using CarMarket.Domain.ViewModels.Car;
using System.Xml.Linq;
using System.Runtime.InteropServices;

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
            try
            {
                var cars = await _carRepository.Select();
                return DataVerification<IEnumerable<Car>>(cars);
            }
            catch(Exception ex) 
            {
                return Error<IEnumerable<Car>>("[GetCars]", ex);
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
                return Error<Car>("[GetCar]", ex);
            }
        }

        public async Task<IBaseResponse<Car>> GetCarByName(string name)
        {
            try
            {
                var car = await _carRepository.GetByName(name);
                return DataVerification<Car>(car);
            }
            catch (Exception ex)
            {
                return Error<Car>("[GetCarByName]",ex);
            }
        }
        public async Task<IBaseResponse<bool>> DeleteCar(int id)
        {
            try
            {
                var car = await _carRepository.Get(id);
                var baseResponce = CheckForNull<bool,Car>(car);
                if(baseResponce != null) 
                {
                    return baseResponce;
                }
                else
                {
                    await _carRepository.Delete(car);
                    baseResponce.Data = true;
                    return baseResponce;
                }
            }
            catch (Exception ex)
            {
                return Error<bool>("[DeleteCar]", ex);
            }
        }

        public async Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel)
        {
            var baseResponse = new BaseResponse<CarViewModel>();
            try
            {
                var car = new Car()
                {
                    Description = carViewModel.Description,
                    DateCreate = DateTime.Now,
                    Speed = carViewModel.Speed,
                    Model = carViewModel.Model,
                    Price = carViewModel.Price,
                    Name = carViewModel.Name,
                    TypeCar =(TypeCar)int.Parse(carViewModel.TypeCar),
                };

                await _carRepository.Create(car);
            }
            catch(Exception ex)
            {
                return Error<CarViewModel>("[CreateCar]", ex);
            }
            return baseResponse;
        }


        public async Task<IBaseResponse<Car>> Edit(int id, CarViewModel carViewModel)
        {
            try
            {
                var car = await _carRepository.Get(id);
                var baseResponce = CheckForNull<Car, Car>(car);
                if (baseResponce != null)
                {
                    return baseResponce;
                }
                else
                {
                    car.Description   = carViewModel.Description;
                    car.DateCreate = carViewModel.DateCreate;
                    car.Speed = carViewModel.Speed;
                    car.Model = carViewModel.Model;
                    car.Price = carViewModel.Price;
                    car.Name = carViewModel.Name;

                    await _carRepository.Update(car);

                    return baseResponce;
                }
            }
            catch (Exception ex)
            {
                return Error<Car>("[Edit]", ex);
            }
        }



        private BaseResponse<T> Error<T>(string nameMethod,Exception ex)
        {
            return new BaseResponse<T>()
            {
                Description = $"{nameMethod} : {ex.Message}",
                StatusCode = StatusCode.IntrenalServerError
            };
        }

        private IBaseResponse<T> DataVerification<T>(T data)
        {

            var baseResponse = CheckForNull<T,T>(data);
            if (baseResponse.StatusCode == StatusCode.Null)
            {
                baseResponse.Data = data;
                baseResponse.StatusCode = StatusCode.OK;
            }
            return baseResponse;
        }
        private IBaseResponse<T> CheckForNull<T,D>(D data)
        {
            var baseResponse = new BaseResponse<T>()
            {
                StatusCode = StatusCode.Null
            };
            if (data == null)
            {
                baseResponse.Description = "Car not found";
                baseResponse.StatusCode = StatusCode.CarNotFound;
             
            }
            return baseResponse;
        }


    }
}
