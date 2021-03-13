using Microsoft.Extensions.Configuration;

namespace CollectiveMind.Configuration
{
	/// <summary>
	/// Extension methods for the <see cref="IConfiguration"/> interface.
	/// </summary>
	public static class ConfigurationExtensions
	{
		/// <summary>
		/// Retrieves an instance of <typeparam name="T"></typeparam> out of the configuration if present,
		/// or default no match was found. 
		/// </summary>
		/// <param name="configuration">The configuration from which the instance of <typeparam name="T"></typeparam>
		/// is retrieved.</param>
		/// <typeparam name="T">The type of the configuration instance to be retrieved.</typeparam>
		/// <returns>An instance of the specified <typeparam name="T"></typeparam>-type or default if no match
		/// was found.</returns>
		public static T GetConfigurationOrDefault<T>(this IConfiguration configuration)
		{
			var configurationSection = configuration.GetSection(typeof(T).Name);
			return configurationSection.Get<T>();
		}
	}
}