using Core.Domain.Models;
using Core.Domain.Models.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Mapping
{
	public class StatableEntityMap : EntityMap, IEntityTypeConfiguration<StableEntity>
	{

		public void Configure(EntityTypeBuilder<StableEntity> builder)
		{ }
	}
}
