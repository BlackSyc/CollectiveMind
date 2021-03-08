using Microsoft.Extensions.Configuration;

namespace CollectiveMind.Configuration
{
	public static class ConfigurationExtensions
	{
		public static T GetConfigurationOrDefault<T>(this IConfiguration configuration)
		{
			var configurationSection = configuration.GetSection(nameof(T));
			return configurationSection.Get<T>();
		}
	}
}