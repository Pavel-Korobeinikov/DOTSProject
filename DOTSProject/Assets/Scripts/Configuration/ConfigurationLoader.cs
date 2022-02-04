using Configuration.Providers;
using Configuration.Structure;
using Cysharp.Threading.Tasks;

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

		public async UniTask Initialize()
		{
			GameConfiguration = await _configurationProvider.LoadConfiguration();
		}
	}
}