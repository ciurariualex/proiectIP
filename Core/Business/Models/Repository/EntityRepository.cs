using Core.Domain.Business.Models;
using Core.Domain.Business.Models.Interfaces;
using Core.Domain.Models.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Domain.Business.Repository
{
    public class EntityRepository<T> : IGenericRepository<T>
     where T : Entity
    {
        private readonly BaseModel _dbContext;

        public EntityRepository(BaseModel dbContext)
        {
            _dbContext = dbContext;
        }

        public int Count()
        {
            return _dbContext.Set<T>()
                .AsNoTracking()
                .Count();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public async Task<IEnumerable<T>> GetAllActive()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllActive(int pageNumber = 1, int pageSize = 15)
        {
            return await _dbContext.Set<T>().AsNoTracking()
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbContext.Set<T>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<T> GetByQuery(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).SingleOrDefaultAsync();
        }

        public async Task Create(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Guid Id, T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
