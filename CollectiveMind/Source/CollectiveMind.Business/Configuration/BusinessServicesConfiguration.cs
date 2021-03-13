using CollectiveMind.Business.Services;
using CollectiveMind.Business.Services.Arguments;
using Microsoft.Extensions.DependencyInjection;

namespace CollectiveMind.Business.Configuration
{
	public static class BusinessServicesConfiguration
	{
		public static IServiceCollection AddBusinessServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IStatementService, StatementService>();
			serviceCollection.AddTransient<IPositiveArgumentService, PositiveArgumentService>();
			serviceCollection.AddTransient<INegativeArgumentService, NegativeArgumentService>();

			return serviceCollection;
		}
	}
}