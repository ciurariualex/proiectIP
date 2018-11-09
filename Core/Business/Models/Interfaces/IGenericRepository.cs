using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Domain.Business.Models.Interfaces
{
	public interface IGenericRepository<T>
	 where T : class/*, <span class="pl-en">IEntity</span>*/
	{
		IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllActive();

        Task<IEnumerable<T>> GetAllActive(int pageNumber,int pageSize);

		int Count();

		Task<T> GetById(Guid id);

		Task<T> GetByQuery(Expression<Func<T, bool>> predicate);

		Task Create(T entity);

		Task Update(T entity);

		Task Update(Guid id, T entity);

		Task Delete(Guid id);

		Task Delete(T entity);

	}
}
