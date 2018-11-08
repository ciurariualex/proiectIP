using Core.Business.Models.Interfaces;
using Core.Domain.Business.Models;
using Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Business.Models.Repository
{
    public class UserRepository : User, IUserRepository
    {
        public UserRepository(BaseModel baseModel)
            : base(baseModel)
        { }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Task Create(User entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllActive()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllActive(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByQuery(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
