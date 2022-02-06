using Application.Launcher.LaunchScenarios;
using Application.MessageLog;
using Application.MessageLog.LogHandlers;
using Cysharp.Threading.Tasks;
using Model;
using Services;
using Services.Configuration;
using Services.Configuration.Providers.ScriptableObjectConfiguration;
using Services.SceneManagement;
using ViewModel;

namespace Application.Launcher
{
	public class GameLauncher
	{
		private readonly LaunchData _launchData;
		private GameModel _gameModel;
		private IGameViewModel _viewModel;
		private ServiceManager _serviceManager;
		private ILaunchScenario _launchScenario;
		
		private bool _isLaunched;

		public GameLauncher(LaunchData launchData)
		{
			_launchData = launchData;
		}

		// Application entry point 
		public async UniTask Launch()
		{
			if (_isLaunched)
			{
				MessageLogger.LogError("Game already launched.");
				
				return;
			}

			CreateDependencies();
			await RegisterServices();
			await Initialize();
			await StartGame();

			_isLaunched = true;
		}

		private async UniTask RegisterServices()
		{
			await _serviceManager.RegisterService<ISceneService>(new SceneService(_viewModel));

			var configurationProvider = new ScriptableObjectConfigurationProvider(_launchData.ConfigurationPath);
			await _serviceManager.RegisterService<IConfigurationService>(new ConfigurationService(configurationProvider));
		}

		private void CreateDependencies()
		{
			var currentLogger = new UnityMessageLogHandler();
			MessageLogger.LogHandler = currentLogger;

			_serviceManager = new ServiceManager();
			_gameModel = new GameModel();
			_viewModel = new GameViewModel(_gameModel, _serviceManager);
			_launchScenario = new MainMenuLaunchScenario(_serviceManager);
		}

		private async UniTask Initialize()
		{
			//TODO: Load data from saves/server and give it to gameModel
			await _gameModel.Initialize();
		}

		private async UniTask StartGame()
		{
			await _launchScenario.Launch();
		}
	}
}
