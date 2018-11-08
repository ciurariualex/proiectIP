using Core.Business.Models.Interfaces;
using Core.Domain.Business.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core.Models;
using System.Collections.Generic;

namespace Core.Business.Models.Repository
{
    public class CarRepository : StableRepository<Car>, ICarRepository
    {
        protected readonly BaseModel _dbcontext;

        public CarRepository(BaseModel dbContext)
            : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public override async Task<Car> GetById(Guid id)
        {
            return await _dbcontext.Set<Car>()
                        .AsNoTracking()
                        .Include(car => car.Showroom)
                        .FirstOrDefaultAsync(car => car.Id.Equals(id));
        }

        public override async Task<IEnumerable<Car>> GetAllActive()
        {
            return await _dbcontext.Set<Car>()
                .AsNoTracking()
                .Include(car => car.Showroom)
                .Where(car => !car.IsDeleted)
                .ToListAsync();
        }

        public Task<Car> GetByVIN(string vin)
        {
            return _dbcontext.Set<Car>()
                .AsNoTracking()
                .Where(car => car.VIN.Equals(vin))
                .FirstOrDefaultAsync();
        }
    }
}
