using System.Threading.Tasks;
using Application.Launcher.LaunchScenarios;
using Application.MessageLog;
using Application.MessageLog.LogHandlers;
using Configuration;
using Configuration.Providers.ScriptableObjectConfiguration;
using Cysharp.Threading.Tasks;
using Model;
using ViewModel.SceneManagement;

namespace Application.Launcher
{
	public class GameLauncher
	{
		private readonly LaunchData _launchData;
		private ConfigurationLoader _configurationLoader;
		private GameModel _gameModel;
		private SceneManager _sceneManager;
		private ILaunchScenario _launchScenario;
		
		private bool _isLaunched;

		public GameLauncher(LaunchData launchData)
		{
			_launchData = launchData;
		}

		public async UniTask Launch()
		{
			if (_isLaunched)
			{
				MessageLogger.LogError("Game already launched.");
			}

			CreateDependencies();
			await Initialize();
			await StartGame();

			_isLaunched = true;
		}

		private void CreateDependencies()
		{
			var currentLogger = new UnityMessageLogHandler();
			MessageLogger.LogHandler = currentLogger;

			var configurationProvider = new ScriptableObjectConfigurationProvider(_launchData.ConfigurationPath);
			_configurationLoader = new ConfigurationLoader(configurationProvider);
			
			_gameModel = new GameModel();
			_sceneManager = new SceneManager();
			_launchScenario = new MainMenuLaunchScenario(_sceneManager, _configurationLoader);
		}

		private async UniTask Initialize()
		{
			await _configurationLoader.Initialize();
			//TODO: Load data from saves/server and give it to gameModel
			await _gameModel.Initialize();
		}

		private async UniTask StartGame()
		{
			await _launchScenario.Launch();
		}
	}
}
