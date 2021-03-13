using System;

namespace CollectiveMind.Graph.Exceptions
{
	/// <summary>
	/// Exception indicating a requested entity could not be found.
	/// </summary>
	public class EntityNotFoundException : Exception
	{
		/// <summary>
		/// Default constructor for creating a new instance of <see cref="EntityNotFoundException"/>.
		/// </summary>
		/// <param name="identifier">The identifier of the entity that could not be found.</param>
		/// <param name="entityType">The type of the entity that could not be found.</param>
		public EntityNotFoundException(Guid identifier, Type entityType) 
			: base($"Entity of type '{entityType}' with identifier '{identifier}' could not be found."){}

	}
}