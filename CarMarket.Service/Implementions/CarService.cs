
using CarMarket.DAL.Interfaces;
using CarMarket.Domain.Enum;
using CarMarket.Domain.Models_Entity_;
using CarMarket.Domain.Response;
using CarMarket.Service.Interface;
using CarMarket.Domain.ViewModels.Car;
using Microsoft.EntityFrameworkCore;
using CarMarket.Domain.Extensions;

namespace CarMarket.Service.Implementions
{
    public class CarService : ICarService
    {
        private readonly IBaseRepository<Car> _carRepository;

        public CarService(IBaseRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public BaseResponse<Dictionary<int,string>> GetTypes()
        {
            try
            {
                var types = ((TypeCar[])Enum.GetValues(typeof(TypeCar))).ToDictionary(k => (int)k, t => t.GetDisplayName());

                 return new BaseResponse<Dictionary<int, string>>()
                 {
                     Data = types,
                     StatusCode = StatusCode.OK
                 };
            }
            catch(Exception ex)
            {
                return Error<Dictionary<int, string>>("[GetTypes]", ex);
            }
        }


        public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
        {
            try
            {
                var cars = _carRepository.GetAll().ToList();
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
                var car = await _carRepository.GetAll().SingleOrDefaultAsync(x => x.Id == id);
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
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Name == name);
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
                var car =  await _carRepository.GetAll().SingleOrDefaultAsync(x => x.Id == id);
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
        public async Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel, byte[] img)
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
                    Avatar = img,
            
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
                var car = await _carRepository.GetAll().SingleOrDefaultAsync(x => x.Id == id);
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
