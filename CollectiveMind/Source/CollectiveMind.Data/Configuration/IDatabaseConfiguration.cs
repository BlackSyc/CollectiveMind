namespace CollectiveMind.Data.Configuration
{
	/// <summary>
	/// Contains configuration variables needed to connect to a database.
	/// </summary>
	public interface IDatabaseConfiguration
	{
		/// <summary>
		/// The connection string used to open the connection to a database.
		/// </summary>
		string ConnectionString { get; }
		
		/// <summary>
		/// The version of the database that will be connected to.
		/// </summary>
		string DatabaseVersion { get; }
	}
}