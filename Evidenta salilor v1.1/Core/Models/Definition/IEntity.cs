using System;

namespace Core.Domain.Definition
{
	public interface IEntity : IAbstractEntity
	{
		new Guid Id { get; set; }
	}
}
