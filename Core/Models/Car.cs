using Core.Domain.Definition;
using Core.Domain.Models.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class Car: StableEntity, IDeletable
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string VIN { get; set; }

        public Guid? ShowroomId { get; set; }

        public virtual Showroom Showroom { get; set; }

    }
}
