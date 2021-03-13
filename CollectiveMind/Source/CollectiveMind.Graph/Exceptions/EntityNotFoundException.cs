using System;

namespace CollectiveMind.Graph.Exceptions
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException(Guid identifier, Type entityType) : base(
			$"Entity of type '{entityType}' with identifier '{identifier}' could not be found."){}

	}
}