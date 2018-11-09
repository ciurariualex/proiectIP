using Core.Domain.Business.Models;
using Core.Domain.Business.Models.Interfaces;
using Core.Domain.Models.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Business.Models.Repository
{
	public class StableRepository<T> : IGenericRepository<T>
	 where T : StableEntity/*, <span class="pl-en">IEntity</span>*/
	{
		protected readonly BaseModel _dbContext;

		public StableRepository(BaseModel dbContext)
		{
			_dbContext = dbContext;
		}

		public int Count()
		{
			return _dbContext.Set<T>()
				.AsNoTracking()
				.Where(value => !value.IsDeleted)
				.Count();
		}

		public virtual IEnumerable<T> GetAll()
		{
			return _dbContext.Set<T>().AsNoTracking();
		}

		public virtual async Task<IEnumerable<T>> GetAllActive()
		{
			return await _dbContext.Set<T>().AsNoTracking().Where(value => !value.IsDeleted).ToListAsync();
		}


		public virtual async Task<IEnumerable<T>> GetAllActive(int pageNumber = 1, int pageSize = 15)
		{
			return await _dbContext.Set<T>().AsNoTracking()
				.Where(value => !value.IsDeleted)
				.Skip(pageSize * (pageNumber - 1))
				.Take(pageSize)
                .ToListAsync();
		}

		public virtual async Task<T> GetById(Guid id)
		{
			return await _dbContext.Set<T>()
						.AsNoTracking()
						.FirstOrDefaultAsync(value => value.Id.Equals(id));
		}

		public virtual async Task<T> GetByQuery(Expression<Func<T, bool>> predicate)
		{
			return await _dbContext.Set<T>().Where(predicate).SingleOrDefaultAsync();
		}

		public virtual async Task Create(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task Update(T entity)
		{
			entity.Edited = DateTime.Now;
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task Update(Guid Id, T entity)
		{
			entity.Edited = DateTime.Now;
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task Delete(Guid id)
		{
			var entity = await GetById(id);
			entity.IsDeleted = true;
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task Delete(T entity)
		{
			entity.IsDeleted = true;
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task Remove(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}
	}
}
