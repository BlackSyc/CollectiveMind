using Ardalis.GuardClauses;
using CollectiveMind.Data.DataContext;
using CollectiveMind.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CollectiveMind.Data.Configuration
{
	public static class DataServicesConfiguration
	{
		public static IServiceCollection AddDataServices(
			this IServiceCollection serviceCollection, 
			IDatabaseConfiguration collectiveMindDatabaseConfiguration)
		{
			Guard.Against.Default(serviceCollection, nameof(serviceCollection));
			Guard.Against.Default(collectiveMindDatabaseConfiguration, nameof(collectiveMindDatabaseConfiguration));
			
			serviceCollection.AddDbContext<CollectiveMindContext>(options =>
			{
				options.EnableSensitiveDataLogging();
				options.UseMySql(collectiveMindDatabaseConfiguration.ConnectionString,
					ServerVersion.FromString(collectiveMindDatabaseConfiguration.DatabaseVersion),
					builder => builder.EnableRetryOnFailure());
			});

			serviceCollection.AddTransient<IStatementRepository, StatementRepository>();

			return serviceCollection;
		}
	}
}