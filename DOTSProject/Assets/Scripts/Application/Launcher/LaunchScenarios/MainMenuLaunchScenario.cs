using Configuration;
using Cysharp.Threading.Tasks;
using ViewModel.SceneManagement;

namespace Application.Launcher.LaunchScenarios
{
	public class MainMenuLaunchScenario : ILaunchScenario
	{
		private readonly SceneManager _sceneManager;
		private readonly ConfigurationLoader _configurationLoader;

		public MainMenuLaunchScenario(
			SceneManager sceneManager,
			ConfigurationLoader configurationLoader)
		{
			_sceneManager = sceneManager;
			_configurationLoader = configurationLoader;
		}

		public async UniTask Launch()
		{
			await _sceneManager.ActivateScene(_configurationLoader.GameConfiguration.MainScene, ActivationSceneMode.Single);
		}
	}
}