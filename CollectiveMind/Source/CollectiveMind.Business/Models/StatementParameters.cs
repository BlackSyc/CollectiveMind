using CollectiveMind.Graph.Entities.Nodes;

namespace CollectiveMind.Business.Models
{
	/// <summary>
	/// Represents the parameters that can be made into a new <see cref="Statement"/>.
	/// </summary>
	public class StatementParameters
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