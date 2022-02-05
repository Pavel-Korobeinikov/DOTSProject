using Application.Launcher.LaunchScenarios;
using Application.MessageLog;
using Application.MessageLog.LogHandlers;
using Configuration.Providers.ScriptableObjectConfiguration;
using Cysharp.Threading.Tasks;
using Model;
using Services;
using Services.Configuration;
using Services.SceneManagement;

namespace Application.Launcher
{
	public class GameLauncher
	{
		private readonly LaunchData _launchData;
		private ConfigurationService _configurationService;
		private GameModel _gameModel;
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
			}

			CreateDependencies();
			await RegisterServices();
			await Initialize();
			await StartGame();

			_isLaunched = true;
		}

		private async UniTask RegisterServices()
		{
			await _serviceManager.RegisterService<ISceneService>(new SceneService());

			var configurationProvider = new ScriptableObjectConfigurationProvider(_launchData.ConfigurationPath);
			await _serviceManager.RegisterService<IConfigurationService>(new ConfigurationService(configurationProvider));
		}

		private void CreateDependencies()
		{
			var currentLogger = new UnityMessageLogHandler();
			MessageLogger.LogHandler = currentLogger;

			var configurationProvider = new ScriptableObjectConfigurationProvider(_launchData.ConfigurationPath);
			_configurationService = new ConfigurationService(configurationProvider);
			
			_serviceManager = new ServiceManager();
			_gameModel = new GameModel();
			_launchScenario = new MainMenuLaunchScenario(_serviceManager, _configurationService);
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
