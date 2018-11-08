using System;

namespace Core.Domain.Models.Implementation
{
	public abstract class AbstractEntity<TIdentity> : IEquatable<AbstractEntity<Guid>>
	{
		public abstract Guid Id { get; set; }

		public static bool operator ==(AbstractEntity<TIdentity> a, AbstractEntity<TIdentity> b)
		{
			if (ReferenceEquals(a, b))
			{
				return true;
			}

			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			return a.Id.Equals(b.Id);
		}

		public static bool operator !=(AbstractEntity<TIdentity> a, AbstractEntity<TIdentity> b)
		{
			return !(a == b);
		}

		public virtual bool Equals(AbstractEntity<Guid> other)
		{
			if (null == other || !this.GetType().IsInstanceOfType(other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			bool otherIsTransient = object.Equals(other.Id, default(Guid));

			bool thisIsTransient = this.IsTransient();
			if (otherIsTransient && thisIsTransient)
			{
				return ReferenceEquals(other, this);
			}

			return other.Id.Equals(this.Id);
		}

		public override bool Equals(object obj)
		{
			var that = obj as AbstractEntity<Guid>;
			return this.Equals(that);
		}

		public override int GetHashCode()
		{
			return this.IsTransient() ? base.GetHashCode() : this.Id.GetHashCode();
		}

		public virtual bool IsTransient()
		{
			return object.Equals(this.Id, default(TIdentity));

		}
	}
}
