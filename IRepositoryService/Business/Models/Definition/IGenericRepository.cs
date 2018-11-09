using System;
using System.Linq;
using System.Threading.Tasks;

namespace IRepositoryServicere.Domain.Business.Models.Definition
{
	public interface IGenericRepository<T>
	 where T : class/*, <span class="pl-en">IEntity</span>*/
	{
		IQueryable<T> GetAll();

		IQueryable<T> GetAllActive();

		Task<T> GetById(Guid id);

		Task Create(T entity);

		Task Update(T entity);

		Task Update(Guid id, T entity);

		Task Delete(Guid id);

		Task Delete(T entity);

		Task Remove(T entity);
	}
}
