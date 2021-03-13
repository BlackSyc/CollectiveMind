using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CollectiveMind.Data.DataContext.Factories
{
	/// <summary>
	/// Design-time factory used to create migrations.
	/// </summary>
	public class CollectiveMindContextFactory : IDesignTimeDbContextFactory<CollectiveMindContext>
	{
		/// <summary>
		/// Creates a new <see cref="CollectiveMindContext"/> instance for design-time use for migrations.
		/// </summary>
		/// <returns>The newly created context.</returns>
		public CollectiveMindContext CreateDbContext(string[] _)
		{
			var optionsBuilder = new DbContextOptionsBuilder<CollectiveMindContext>();

			optionsBuilder.UseMySql(
				"Server=localhost;Port=1003;database=CollectiveMind;user=root;password=wachtwoord",
				new MySqlServerVersion(new Version(8,1,21)),
				mySqlOptions => mySqlOptions.EnableRetryOnFailure());
			
			return new CollectiveMindContext(optionsBuilder.Options);
		}
	}
}