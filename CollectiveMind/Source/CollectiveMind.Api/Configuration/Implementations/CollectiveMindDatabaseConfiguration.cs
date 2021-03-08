using CollectiveMind.Data.Configuration;

namespace CollectiveMind.Configuration.Implementations
{
	public class CollectiveMindDatabaseConfiguration : IDatabaseConfiguration
	{
		public string ConnectionString { get; set; }
		public string DatabaseVersion { get; set; }
	}
}