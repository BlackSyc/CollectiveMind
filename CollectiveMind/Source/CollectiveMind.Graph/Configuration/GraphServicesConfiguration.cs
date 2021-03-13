using System;
using Ardalis.GuardClauses;
using CollectiveMind.Graph.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver;

namespace CollectiveMind.Graph.Configuration
{
	/// <summary>
	/// Configuration extensions for the graph services in this project.
	/// </summary>
	public static class GraphServicesConfiguration
	{
		/// <summary>
		/// Registers graph services to the specified service collection.
		/// </summary>
		/// <param name="serviceCollection">The service collection to which the graph services
		/// will be registered.</param>
		/// <param name="graphConnectionConfiguration">The configuration required to connect to the
		/// graph.</param>
		/// <returns>The service collection to which the services were registered.</returns>
		public static IServiceCollection RegisterGraphServices(
			this IServiceCollection serviceCollection, 
			IGraphConnectionConfiguration graphConnectionConfiguration)
		{
			Guard.Against.Default(serviceCollection, nameof(serviceCollection));
			Guard.Against.Default(graphConnectionConfiguration, nameof(graphConnectionConfiguration));

			serviceCollection.AddSingleton(
				GraphDatabase.Driver(
					new Uri($"bolt://{graphConnectionConfiguration.Url}"),
				AuthTokens.Basic(graphConnectionConfiguration.Username, graphConnectionConfiguration.Password)));

			serviceCollection.AddTransient<IStatementRepository, StatementRepository>();

			return serviceCollection;
		}
	}
}