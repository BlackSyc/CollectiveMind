using CollectiveMind.Graph.Configuration;

namespace CollectiveMind.Configuration.Implementations
{
	public class GraphConnectionDetails : IGraphConnectionDetails
	{
		public string Url { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}