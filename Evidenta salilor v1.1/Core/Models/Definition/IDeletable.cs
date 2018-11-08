namespace Core.Domain.Definition
{
	public interface IDeletable
	{
		bool IsDeleted { get; set; }
	}
}
