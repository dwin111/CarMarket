
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
        Task<IBaseResponse<Car>> Edit(int id, CarViewModel carViewModel);
        Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel, byte[] img);
        Task<IBaseResponse<bool>> DeleteCar(int id);
        BaseResponse<Dictionary<int, string>> GetTypes();

    }
}
