using CollectiveMind.Business.Configuration;
using CollectiveMind.Configuration;
using CollectiveMind.Configuration.Implementations;
using CollectiveMind.Data.Configuration;
using CollectiveMind.Graph.Configuration;
using CollectiveMind.Middleware;
using CollectiveMind.Swagger;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CollectiveMind
{
	/// <summary>
	/// Startup class containing service and application configuration calls that are executed on startup.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// The configuration variables used for startup.
		/// May contain app settings variables or environment variables.
		/// </summary>
		private readonly IConfiguration _configuration;

		/// <summary>
		/// The current environment used when starting the application.
		/// </summary>
		private readonly IWebHostEnvironment _currentEnvironment;
		
		/// <summary>
		/// Default constructor for creating a new instance of <see cref="Startup"/>.
		/// </summary>
		/// <param name="configuration">The configuration variables used for startup.</param>
		/// <param name="currentEnvironment">The environment the application will start in.</param>
		public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
		{
			_configuration = configuration;
			_currentEnvironment = currentEnvironment;
		}

		/// <summary>
		/// Configure services and add them to the service collection.
		/// </summary>
		/// <param name="services">The service collection to which services should be added.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.RegisterBusinessServices();
			services.RegisterDataServices(_configuration.GetConfigurationOrDefault<CollectiveMindDatabaseConfiguration>());
			services.RegisterGraphServices(_configuration.GetConfigurationOrDefault<GraphConnectionConfiguration>());
			
			services.AddControllers();
			services.AddSwaggerServices();
			services.AddExceptionMiddleware(_currentEnvironment.IsDevelopment());
			
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
					builder
						.WithOrigins(new []{"http://localhost:4200"})
						.AllowAnyHeader()
						.AllowAnyMethod()
						.SetIsOriginAllowedToAllowWildcardSubdomains());
			});
		}

		/// <summary>
		/// Configures the applications HTTP request pipeline.
		/// </summary>
		/// <param name="app">The application for which the HTTP request pipeline will be configured.</param>
		public void Configure(IApplicationBuilder app)
		{
			if (_currentEnvironment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CollectiveMind.Api v1"));

			app.UseProblemDetails();
			app.UseRouting();
			app.UseCors();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
			
			app.ConfigureApplicationData();
		}
	}
}