﻿
using CarMarket.Domain.Models_Entity_;
using CarMarket.Domain.ViewModels.Car;
using CarMarket.Domain.Response;

namespace CarMarket.Service.Interface
{
    public interface ICarService
    {
        Task<IBaseResponse<IEnumerable<Car>>> GetCars();

        Task<IBaseResponse<Car>> GetCar(int id);
        Task<IBaseResponse<Car>> GetCarByName(string name);
        Task<IBaseResponse<bool>> DeleteCar(int id);
        Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel);

    }
}