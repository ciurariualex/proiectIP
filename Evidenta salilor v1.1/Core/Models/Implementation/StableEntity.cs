using Core.Domain.Definition;
using System;

namespace Core.Domain.Models.Implementation
{
	public class StableEntity : Entity, IDeletable
	{
		public DateTime Created { get; set; } = DateTime.UtcNow;
		public DateTime Edited { get; set; } = DateTime.Now;
		public bool IsDeleted { get; set; }
	}
}
