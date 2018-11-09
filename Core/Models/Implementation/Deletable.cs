using Core.Domain.Definition;

namespace Core.Domain.Models.Implementation
{
	public class Deletable : IDeletable
	{
		public bool IsDeleted { get; set; } = false;
	}
}
