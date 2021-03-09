using System;
using Ardalis.GuardClauses;
using CollectiveMind.Data.DataContext;
using CollectiveMind.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

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

		public static IApplicationBuilder UseDataServices(this IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.MigrateDatabase<CollectiveMindContext>();
			return applicationBuilder;
		}
		
		private static void MigrateDatabase<TContext>(this IApplicationBuilder app)
			where TContext : DbContext
		{
			using var scope = app.ApplicationServices.CreateScope();

			var dataContext = scope.ServiceProvider.GetRequiredService<TContext>();
			var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
			
			Retry<DbUpdateException>(() =>
			{
				logger.LogInformation("Trying to migrate database...");
				dataContext.Database.Migrate();
				logger.LogInformation("Database migration was successful");
			}, 10, 2, logger);
		}

		private static void Retry<T>(Action action, int times, double retryDelay, ILogger logger) 
			where T : Exception
		{
			Policy.Handle<T>()
				.WaitAndRetry(
					times,
					retryAttempt => TimeSpan.FromSeconds(Math.Pow(retryDelay, retryAttempt)),
					(exception, timeSpan, retry, context) =>
					{
						logger.LogWarning(
							exception, 
							// ReSharper disable once TemplateIsNotCompileTimeConstantProblem
							$"{exception.GetType().Name} thrown while trying to migrate " +
							$"database, but will try again in {timeSpan.TotalSeconds}s... ({retry}/{times}");
					}).Execute(action);
		}
	}
}