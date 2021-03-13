using CollectiveMind.Configuration;
using CollectiveMind.Configuration.Implementations;
using CollectiveMind.Data.Configuration;
using CollectiveMind.Graph.Configuration;
using CollectiveMind.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CollectiveMind
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDataServices(Configuration.GetConfigurationOrDefault<CollectiveMindDatabaseConfiguration>());
			services.AddGraphServices(Configuration.GetConfigurationOrDefault<GraphConnectionDetails>());
			
			services.AddControllers();
			services.AddSwaggerServices();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//if (env.IsDevelopment())
			//{
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CollectiveMind.Api v1"));
			//}
			//else
			//{
			//	app.UseHttpsRedirection();
			//}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
			
			app.UseDataServices();
		}
	}
}