using Configuration;
using Cysharp.Threading.Tasks;
using Services;
using Services.Configuration;
using Services.SceneManagement;

namespace Application.Launcher.LaunchScenarios
{
	public class MainMenuLaunchScenario : ILaunchScenario
	{
		private readonly IServiceManager _serviceManager;
		private readonly ConfigurationService _configurationService;

		public MainMenuLaunchScenario(
			IServiceManager serviceManager,
			ConfigurationService configurationService)
		{
			_serviceManager = serviceManager;
			_configurationService = configurationService;
		}

		public async UniTask Launch()
		{
			var sceneService = _serviceManager.GetService<ISceneService>();
			await sceneService.ActivateScene(_configurationService.GameConfiguration.MainScene, ActivationSceneMode.Single);
		}
	}
}