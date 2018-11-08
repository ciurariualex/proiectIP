using IAnimalService.Models;

namespace IAnimalService.Business.Models.Implementation
{
	public class AnimalRepository : StableModel<Animal>, IAnimalRepository
	{
		public AnimalRepository(BaseModel dbContext)
			: base(dbContext)
		{ }

	}
}
