using System;
using Ardalis.GuardClauses;
using CollectiveMind.Graph.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver;

namespace CollectiveMind.Graph.Configuration
{
	public static class GraphServicesConfiguration
	{
		public static IServiceCollection AddGraphServices(
			this IServiceCollection serviceCollection, 
			IGraphConnectionDetails graphConnectionDetails)
		{
			Guard.Against.Default(serviceCollection, nameof(serviceCollection));
			Guard.Against.Default(graphConnectionDetails, nameof(graphConnectionDetails));

			serviceCollection.AddSingleton(
				GraphDatabase.Driver(
					new Uri($"bolt://{graphConnectionDetails.Url}"),
				AuthTokens.Basic(graphConnectionDetails.Username, graphConnectionDetails.Password)));

			serviceCollection.AddTransient<IStatementNodeRepository, StatementNodeRepository>();

			return serviceCollection;
		}
	}
}