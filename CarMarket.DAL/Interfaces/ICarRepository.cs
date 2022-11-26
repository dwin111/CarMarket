using CarMarket.Domain.Models_Entity_;

namespace CarMarket.DAL.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        Task<Car> GetByName(string name);
    }
}
