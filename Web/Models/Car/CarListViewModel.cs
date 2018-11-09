using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using car = Core.Models.Car;

namespace Web.Models.Car
{
    public class CarListViewModel : Pager<car>
    {
        public CarListViewModel(IEnumerable<car> cars, int pageNumber, int pageSize)
            : base(cars.AsQueryable(), pageNumber, pageSize)
        { }
    }
}
