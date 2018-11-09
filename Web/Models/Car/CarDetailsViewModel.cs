using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showroom = Core.Models.Showroom;

namespace Web.Models.Car
{
    public class CarDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string VIN { get; set; }

        public showroom Showroom { get; set; }
    }
}
