using Core.Business.Mapping;
using Core.Mapping;
using Core.Models;
using Core.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.Business.Models
{
	public class BaseModel : IdentityDbContext<User>
	{
		public BaseModel(DbContextOptions<BaseModel> options)
			: base(options)
		{ }
        public DbSet<Showroom> Showrooms { get; set; }

        public DbSet<Car> Cars { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);


            builder.ApplyConfiguration<Showroom>(new ShowroomMap());

			builder.ApplyConfiguration<Car>(new CarMap());

		}

	}
}
