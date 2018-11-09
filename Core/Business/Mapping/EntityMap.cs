using Core.Domain.Models;
using Core.Domain.Models.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Mapping
{
	public class EntityMap : IEntityTypeConfiguration<Entity>
	{
		public void Configure(EntityTypeBuilder<Entity> builder)
		{
			builder.Property(value => value.Id).ValueGeneratedOnAdd();
		}

	}
}
