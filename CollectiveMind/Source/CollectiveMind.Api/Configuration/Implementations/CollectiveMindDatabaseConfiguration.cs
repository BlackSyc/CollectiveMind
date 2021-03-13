using CollectiveMind.Data.Configuration;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace CollectiveMind.Configuration.Implementations
{
	/// <summary>
	/// Implementation of the database connection configuration.
	/// </summary>
	public class CollectiveMindDatabaseConfiguration : IDatabaseConfiguration
	{
		/// <inheritdoc />
		public string ConnectionString { get; set; }
		
		/// <inheritdoc />
		public string DatabaseVersion { get; set; }
	}
}