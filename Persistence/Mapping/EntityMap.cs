using Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping
{
	public class EntityMap : IEntityTypeConfiguration<Entity>
	{
		public void Configure(EntityTypeBuilder<Entity> builder)
		{
			builder.Property(value => value.Id).ValueGeneratedOnAdd();
		}

	}
}
