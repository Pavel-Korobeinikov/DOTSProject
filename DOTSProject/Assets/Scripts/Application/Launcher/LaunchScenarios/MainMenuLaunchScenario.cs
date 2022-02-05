using Configuration;
using Cysharp.Threading.Tasks;
using Services;
using Services.SceneManagement;

namespace Application.Launcher.LaunchScenarios
{
	public class MainMenuLaunchScenario : ILaunchScenario
	{
		private readonly IServiceManager _serviceManager;
		private readonly ConfigurationLoader _configurationLoader;

		public MainMenuLaunchScenario(
			IServiceManager serviceManager,
			ConfigurationLoader configurationLoader)
		{
			_serviceManager = serviceManager;
			_configurationLoader = configurationLoader;
		}

		public async UniTask Launch()
		{
			var sceneService = _serviceManager.GetService<ISceneService>();
			await sceneService.ActivateScene(_configurationLoader.GameConfiguration.MainScene, ActivationSceneMode.Single);
		}
	}
}