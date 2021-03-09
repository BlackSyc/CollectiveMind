namespace CollectiveMind.Graph.Configuration
{
	public interface IGraphConnectionDetails
	{
		string Url { get; }
		
		string Username { get; }
		
		string Password { get; }
	}
}