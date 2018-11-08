using IRepositoryServicere.Domain.Business.Models.Definition;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRepositoryService.Business.Models.Implementation
{
	public class StableModel<T> : IGenericRepository<T>
	 where T : StableEntity/*, <span class="pl-en">IEntity</span>*/
	{
		private readonly BaseModel _dbContext;

		public StableModel(BaseModel dbContext)
		{
			_dbContext = dbContext;
		}

		public IQueryable<T> GetAll()
		{
			return _dbContext.Set<T>().AsNoTracking();
		}

		public IQueryable<T> GetAllActive()
		{
			return _dbContext.Set<T>().AsNoTracking().Where(a => a.IsDeleted == false);
		}

		public async Task<T> GetById(Guid id)
		{
			return await _dbContext.Set<T>()
						.AsNoTracking()
						.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task Create(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task Update(T entity)
		{
			entity.Edited = DateTime.Now;
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task Update(Guid Id, T entity)
		{
			entity.Edited = DateTime.Now;
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task Delete(Guid id)
		{
			var entity = await GetById(id);
			entity.IsDeleted = true;
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task Delete(T entity)
		{
			entity.IsDeleted = true;
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task Remove(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

	}
}
