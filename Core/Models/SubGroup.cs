using Core.Domain.Definition;
using Core.Domain.Models.Implementation;

namespace Core.Models.SubGroup
{
	public class SubGroup : StableEntity, IDeletable
	{
		public int Number { get; set; }

		public int Type { get; set; }

		public int Unknown { get; set; }

		public string Name { get; set; }
	}
}
