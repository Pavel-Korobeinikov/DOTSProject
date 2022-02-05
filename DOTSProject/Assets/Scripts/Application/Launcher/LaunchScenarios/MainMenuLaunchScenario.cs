using Cysharp.Threading.Tasks;
using Services;
using Services.Configuration;
using Services.SceneManagement;

namespace Application.Launcher.LaunchScenarios
{
	public class MainMenuLaunchScenario : ILaunchScenario
	{
		private readonly IServiceManager _serviceManager;

		public MainMenuLaunchScenario(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}

		public async UniTask Launch()
		{
			var sceneService = _serviceManager.GetService<ISceneService>();
			var configurationService = _serviceManager.GetService<IConfigurationService>();
			
			await sceneService.ActivateScene(configurationService.GameConfiguration.MainScene, ActivationSceneMode.Single);
		}
	}
}