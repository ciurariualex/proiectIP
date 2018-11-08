using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Showroom
{
    public class ShowroomDetailsViewModel
    {
        //public ShowroomDetailsViewModel()
        //{
        //}
        //public ShowroomDetailsViewModel(List<Core.Models.Car> cars)
        //{
        //    Cars = cars;
        //}
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Core.Models.Car> Cars { get; set; }
}
}
