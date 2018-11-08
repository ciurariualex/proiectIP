using Core.Domain.Definition;
using Core.Domain.Models.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Showroom : StableEntity, IDeletable
    {
        public string Name { get; set; }
        
        public ICollection<Car> Cars { get; set; }
    }
}
