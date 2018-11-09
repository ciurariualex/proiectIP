using Core.Domain.Models;
using IRepositoryService.Domain.Business.Models.Abstract;
using IRepositoryServicere.Domain.Business.Models.Definition;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IRepositoryService.Domain.Business.Implementation
{
	public class EntityModel<T> : IGenericRepository<T>
	 where T : Entity/*, <span class="pl-en">IEntity</span>*/
	{
		private readonly BaseModel _dbContext;

		public EntityModel(BaseModel dbContext)
		{
			_dbContext = dbContext;
		}

		public IQueryable<T> GetAll()
		{
			return _dbContext.Set<T>().AsNoTracking();
		}

		public IQueryable<T> GetAllActive()
		{
			return _dbContext.Set<T>().AsNoTracking();
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
