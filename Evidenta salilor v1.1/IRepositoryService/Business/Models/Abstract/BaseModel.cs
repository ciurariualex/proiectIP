using IAnimalService.Mapping;
using IAnimalService.Models;
using Microsoft.EntityFrameworkCore;

namespace IRepositoryService.Domain.Business.Models.Abstract
{
	public abstract class BaseModel : DbContext
	{
		public BaseModel(DbContextOptions<BaseModel> options)
			: base(options)
		{ }

		//public DbSet<Category> Categories { get; set; }

		public DbSet<Animal> Animals { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);


			builder.ApplyConfiguration<Animal>(new AnimalMap());
		}

	}
}
