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

    public class ShowroomRepository : StableRepository<Showroom>, IShowroomRepository
    {
        protected readonly BaseModel _dbcontext;
        public ShowroomRepository(BaseModel dbContext)
            : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public Task<Showroom> GetByName(string name)
        {
            return _dbcontext.Set<Showroom>()
                .AsNoTracking()
                .Where(showroom => showroom.Name.Equals(name))
                .FirstOrDefaultAsync();
        }

        public override async Task<Showroom> GetById(Guid id)
        {
            return await _dbContext.Set<Showroom>()
                        .AsNoTracking()
                        .Include(showroom => showroom.Cars)
                        .FirstOrDefaultAsync(showroom => showroom.Id.Equals(id));
        }

        public override async Task<IEnumerable<Showroom>> GetAllActive()
        {
            return await _dbcontext.Set<Showroom>()
                .AsNoTracking()
                .Include(showroom => showroom.Cars)
                .Where(showroom => !showroom.IsDeleted)
                .ToListAsync();
        }
    }

}
