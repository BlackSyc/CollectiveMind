using CollectiveMind.Graph.Configuration;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace CollectiveMind.Configuration.Implementations
{
	/// <summary>
	/// Implementation of the graph connection configuration.
	/// </summary>
	public class GraphConnectionConfiguration : IGraphConnectionConfiguration
	{
		/// <inheritdoc />
		public string Url { get; set; }
		
		/// <inheritdoc />
		public string Username { get; set; }
		
		/// <inheritdoc />
		public string Password { get; set; }
	}
}