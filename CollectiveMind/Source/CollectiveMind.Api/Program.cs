using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
// ReSharper disable ClassNeverInstantiated.Global

namespace CollectiveMind
{
	/// <summary>
	/// Entry class of the application.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Main entrypoint of the application.
		/// </summary>
		/// <param name="args">Any arguments passed to start the application.</param>
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		/// <summary>
		/// Creates a new host builder and defines the startup file to be used for configuration.
		/// </summary>
		/// <param name="args">Any arguments needed to create the host builder.</param>
		/// <returns>The newly created host builder.</returns>
		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
	}
}