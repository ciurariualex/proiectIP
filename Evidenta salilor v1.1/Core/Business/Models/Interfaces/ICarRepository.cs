using Core.Domain.Business.Models.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Models.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        Task<Car> GetByVIN(string name);
    }
}
