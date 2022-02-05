using Application.Launcher.LaunchScenaries;
using Configuration.Structure;
using Cysharp.Threading.Tasks;
using ViewModel.SceneManagement;

namespace Application.Launcher.LaunchScenarios
{
	public class MainMenuLaunchScenario : ILaunchScenario
	{
		private readonly SceneManager _sceneManager;
		private readonly GameConfigurationEntity _gameConfigurationEntity;

		public MainMenuLaunchScenario(
			SceneManager sceneManager,
			GameConfigurationEntity gameConfigurationEntity)
		{
			_sceneManager = sceneManager;
			_gameConfigurationEntity = gameConfigurationEntity;
		}

		public async UniTask Launch()
		{
			await _sceneManager.ActivateScene(_gameConfigurationEntity.Main);
		}
	}
}