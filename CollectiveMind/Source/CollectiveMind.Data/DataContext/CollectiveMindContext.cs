using Microsoft.EntityFrameworkCore;

namespace CollectiveMind.Data.DataContext
{
	/// <summary>
	/// Data context used to communicate with the database using Entity Framework.
	/// 
	/// Migrate this context with
	/// "dotnet ef migrations add MIGRATION_NAME -c CollectiveMindContext -s ./Source/CollectiveMind.Api -p ./Source/CollectiveMind.Data".
	/// </summary>
	public class CollectiveMindContext : DbContext
	{
		public CollectiveMindContext(DbContextOptions<CollectiveMindContext> options) 
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}