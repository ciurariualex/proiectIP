using Core.Domain.Models.Implementation;
using System;

namespace Core.Domain.Definition
{
	public interface IAbstractEntity : IEquatable<AbstractEntity<Guid>>
	{
		Guid Id { get; set; }
		new bool Equals(AbstractEntity<Guid> other);
		bool Equals(object obj);
		bool IsTransient();
	}
}
