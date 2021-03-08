namespace CollectiveMind.Data.Configuration
{
	public interface IDatabaseConfiguration
	{
		string ConnectionString { get; set; }
		
		string DatabaseVersion { get; set; }
	}
}