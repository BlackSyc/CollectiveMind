using System;
using CollectiveMind.Graph.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.DependencyInjection;

namespace CollectiveMind.Middleware
{
	/// <summary>
	/// Configuration methods for the exception handling middleware.
	/// </summary>
	public static class ExceptionMiddlewareConfiguration
	{
		/// <summary>
		/// Registers mappings used to return appropriate problem details when an exception is thrown.
		/// </summary>
		/// <param name="serviceCollection">The service collection to which the problem details services will be
		/// registered.</param>
		/// <param name="isDevelopmentEnvironment">Whether or not the application is running in development mode.
		/// When this is true, additional exception details will be returned in the response.</param>
		/// <returns>The service collection to which the problem details services were registered.</returns>
		public static IServiceCollection AddExceptionMiddleware(
			this IServiceCollection serviceCollection,
			bool isDevelopmentEnvironment)
		{
			return serviceCollection.AddProblemDetails(options =>
			{
				options.IncludeExceptionDetails = (_, _) => isDevelopmentEnvironment;
				
				options.Map<EntityNotFoundException>(ex =>
					new StatusCodeProblemDetails(404)
					{
						Title = "Entity not found",
						Type = ex.GetType().Name,
						Detail = ex.Message
					});
				
				options.Map<InvalidIdentifierException>(ex => 
					new StatusCodeProblemDetails(400)
					{
						Title = "Identifier must be empty",
						Type = ex.GetType().Name,
						Detail = ex.Message
					});

				options.Map<ArgumentException>(ex =>
					new StatusCodeProblemDetails(400)
					{
						Title = "Invalid argument",
						Type = ex.GetType().Name,
						Detail = ex.Message
					});
				
				options.Map<ArgumentNullException>(ex => 
					new StatusCodeProblemDetails(400)
					{
						Title = "Argument is null",
						Type = ex.GetType().Name,
						Detail = ex.Message
					});
			});
		}
	}
}