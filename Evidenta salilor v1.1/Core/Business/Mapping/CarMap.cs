using Core.Domain.Models.Implementation;
using Core.Mapping;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Mapping
{
    class CarMap : StatableEntityMap, IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .HasOne(car => car.Showroom)
                .WithMany(showroom => showroom.Cars); 
        }

    }
}
