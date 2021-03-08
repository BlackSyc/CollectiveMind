using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CollectiveMind.Swagger
{
	public static class SwaggerExtensions
	{
		public static IServiceCollection AddSwaggerServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo {Title = "CollectiveMind.Api", Version = "v1"});
			});

			return serviceCollection;
		}
	}
}