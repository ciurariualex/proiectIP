using System;

namespace Core.Domain.Definition
{
	interface IStableEntity : IEntity, IDeletable
	{
		DateTime Created { get; set; }
		DateTime Edited { get; set; }
	}
}
