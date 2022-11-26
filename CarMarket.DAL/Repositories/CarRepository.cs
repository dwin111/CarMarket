using CarMarket.DAL.Interfaces;
using CarMarket.Domain.Models_Entity_;
using Microsoft.EntityFrameworkCore;

namespace CarMarket.DAL.Repositories
{
    public class CarRepository : ICarRepository
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

        public async Task<Car> Get(int id)
        {
            return await _db.Car.SingleAsync(x => x.Id == id);
        }

        public async Task<Car> GetByName(string name)
        {
            return await _db.Car.FirstOrDefaultAsync(x => x.Name.Equals(name));
        }

        public async Task<List<Car>> Select()
        {
            return await _db.Car.ToListAsync();
        }

        public async Task<Car> Update(Car car)
        {
            _db.Car.Update(car);
            await _db.SaveChangesAsync();
            return car;
        }
    }
}
