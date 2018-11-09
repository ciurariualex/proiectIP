using Core.Mapping;
using IAnimalService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAnimalService.Mapping
{
	public class AnimalMap : StatableEntityMap, IEntityTypeConfiguration<Animal>
	{
		public void Configure(EntityTypeBuilder<Animal> builder)
		{
			builder.Property(value => value.Description).HasDefaultValue("This is a default description");	
		}

	}
}
