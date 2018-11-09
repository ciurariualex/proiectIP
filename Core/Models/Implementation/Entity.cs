using System;

namespace Core.Domain.Models.Implementation
{
	public abstract class Entity : AbstractEntity<Guid>
	{
		public override Guid Id { get; set; }

	}
}
