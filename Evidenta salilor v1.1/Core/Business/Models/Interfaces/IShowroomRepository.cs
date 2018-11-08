using Core.Domain.Business.Models.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Models.Interfaces
{
    public interface IShowroomRepository : IGenericRepository<Showroom>
    {
        Task<Showroom> GetByName(string name);
    }
}
