using Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping
{
	public class AnimalMap : EntityMap, IEntityTypeConfiguration<Animal>
	{
		public void Configure(EntityTypeBuilder<Animal> builder)
		{
			builder.Property(value => value.Description).HasDefaultValue("This is a default description");
		}

	}
}
