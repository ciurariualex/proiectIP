using Core.Domain.Models;
using IRepositCoreoryService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IRepositoryService.Mapping
{
	public class StatableEntityMap : EntityMap, IEntityTypeConfiguration<StableEntity>
	{

		public void Configure(EntityTypeBuilder<StableEntity> builder)
		{ }
	}
}
