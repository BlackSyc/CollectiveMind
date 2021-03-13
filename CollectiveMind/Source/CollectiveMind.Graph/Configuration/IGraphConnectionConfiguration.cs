namespace CollectiveMind.Graph.Configuration
{
	/// <summary>
	/// Contains configuration variables needed to connect to a graph.
	/// </summary>
	public interface IGraphConnectionConfiguration
	{
		/// <summary>
		/// The URL to the graph that will be connected to.
		/// </summary>
		string Url { get; }
		
		/// <summary>
		/// The username used for authorization for the graph.
		/// </summary>
		string Username { get; }
		
		/// <summary>
		/// The password used for authorization for the graph.
		/// </summary>
		string Password { get; }
	}
}