using System.Threading.Tasks;
using Configuration.Providers;
using Configuration.Structure;

namespace Configuration
{
	public class ConfigurationLoader
	{
		private readonly IConfigurationProvider _configurationProvider;

		public GameConfiguration GameConfiguration { get; set; }

		public ConfigurationLoader(IConfigurationProvider configurationProvider)
		{
			_configurationProvider = configurationProvider;
		}

		public async Task Initialize()
		{
			GameConfiguration = await _configurationProvider.LoadConfiguration();
		}
	}
}