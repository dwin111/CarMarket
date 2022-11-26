using CarMarket.DAL.Interfaces;
using CarMarket.Domain.Models_Entity_;
using Microsoft.EntityFrameworkCore;

namespace CarMarket.DAL.Repositories
{
    public class CarRepository : IBaseRepository<Car>
    {
        private readonly ApplicationDbContext _db;

        public CarRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Car model)
        {
            if (model != null)
            {
                await _db.Car.AddAsync(model);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(Car model)
        {

            if (model != null)
            {
                _db.Car.Remove(model);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<Car> GetAll()
        {
            return _db.Car;
        }

        public async Task<Car> Update(Car car)
        {
            _db.Car.Update(car);
            await _db.SaveChangesAsync();
            return car;
        }
    }
}
