﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CollectiveMind.Swagger
{
	/// <summary>
	/// Extension methods used to configure Swagger.
	/// </summary>
	public static class SwaggerExtensions
	{
		/// <summary>
		/// Adds swagger services to the specified service collection.
		/// </summary>
		/// <param name="serviceCollection">The service collection to which the swagger services will be added.</param>
		/// <returns>The service collection to which the swagger services were added.</returns>
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