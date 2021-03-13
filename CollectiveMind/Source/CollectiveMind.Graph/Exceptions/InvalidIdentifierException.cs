using System;

namespace CollectiveMind.Graph.Exceptions
{
	/// <summary>
	/// Exception indicating an invalid identifier was specified.
	/// </summary>
	public class InvalidIdentifierException : Exception
	{
		/// <summary>
		/// Default constructor for creating a new instance of <see cref="InvalidIdentifierException"/>.
		/// </summary>
		/// <param name="identifier">The identifier that was found invalid.</param>
		public InvalidIdentifierException(Guid identifier) 
			: base($"Identifier must be empty, but was '{identifier}'."){}
	}
}