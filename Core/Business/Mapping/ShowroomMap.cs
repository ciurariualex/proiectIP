using Core.Mapping;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Mapping
{
    class ShowroomMap : StatableEntityMap, IEntityTypeConfiguration<Showroom>
    {
        public void Configure(EntityTypeBuilder<Showroom> builder)
        {
            builder
                .HasMany(showroom => showroom.Cars)
                .WithOne(car => car.Showroom);
        }

    }
}
