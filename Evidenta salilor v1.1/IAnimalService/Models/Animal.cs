using Core.Domain.Models;

namespace IAnimalService.Models
{
	public class Animal : StableEntity
	{
		public string Name { get; set; }
		public string Color { get; set; }
		public string Description { get; set; }
	}
}
