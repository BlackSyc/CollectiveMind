// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace CollectiveMind.Graph.Entities.Nodes
{
	/// <summary>
	/// Represents a statement node.
	/// </summary>
	public class Statement : Node
	{
		/// <summary>
		/// A brief description of the subject of the statement.
		/// </summary>
		public string Title { get; set; }
		
		/// <summary>
		/// A full explanation of the statement.
		/// </summary>
		public string Body { get; set; }
	}
}