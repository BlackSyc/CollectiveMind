using CollectiveMind.Business.Services.Statements;
using CollectiveMind.Business.Services.Statements.Arguments;
using Microsoft.Extensions.DependencyInjection;

namespace CollectiveMind.Business.Configuration
{
	/// <summary>
	/// Configuration extensions for the business services in this project.
	/// </summary>
	public static class BusinessServicesConfiguration
	{
		/// <summary>
		/// Registers business services to the specified service collection.
		/// </summary>
		/// <param name="serviceCollection">The service collection to which the business services
		/// will be registered.</param>
		/// <returns>The service collection to which the services were registered.</returns>
		public static IServiceCollection RegisterBusinessServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IStatementService, StatementService>();
			serviceCollection.AddTransient<IPositiveArgumentService, PositiveArgumentService>();
			serviceCollection.AddTransient<INegativeArgumentService, NegativeArgumentService>();

			return serviceCollection;
		}
	}
}