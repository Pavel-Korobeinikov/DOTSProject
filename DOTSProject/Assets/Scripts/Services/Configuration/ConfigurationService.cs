using Configuration.Providers;
using Configuration.Structure;
using Cysharp.Threading.Tasks;

namespace Services.Configuration
{
	public class ConfigurationService : IConfigurationService, IInitializable
	{
		private readonly IConfigurationProvider _configurationProvider;

		public GameConfigurationEntity GameConfiguration { get; private set; }

		public ConfigurationService(IConfigurationProvider configurationProvider)
		{
			_configurationProvider = configurationProvider;
		}

		public async UniTask Initialize()
		{
			GameConfiguration = await _configurationProvider.LoadConfiguration();
		}
	}
}