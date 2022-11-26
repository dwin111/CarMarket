
using CarMarket.Domain.Models_Entity_;

namespace CarMarket.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T model);

        Task<T> Get(int id);

        Task<List<Car>> Select();

        Task<bool> Delete(T model);
        Task<T> Update(T car);
    }
}
