using System.Threading.Tasks;
using Application.MessageLog;
using Application.MessageLog.LogHandlers;
using Configuration;
using Configuration.Providers;
using Model;
using UnityEngine;
using ViewModel;

namespace Application
{
	public class GameLauncher
	{
		private ConfigurationLoader _configurationLoader;
		private GameModel _gameModel;
		private GameViewModel _gameViewModel;
		
		private bool _isLaunched;
    
		public async Task Launch()
		{
			if (_isLaunched)
			{
				MessageLogger.LogError("Game already launched.");
			}

			CreateDependencies();
			await Initialize();

			_isLaunched = true;
		}

		private void CreateDependencies()
		{
			var currentLogger = new UnityMessageLogHandler();
			MessageLogger.LogHandler = currentLogger;

			var configurationProvider = new ScriptableObjectConfigurationProvider();
			_configurationLoader = new ConfigurationLoader(configurationProvider);
			
			_gameModel = new GameModel();
			_gameViewModel = new GameViewModel(_gameModel);
		}

		private async Task Initialize()
		{
			await _configurationLoader.Initialize();
			//TODO: Load data from saves/server and give it to gameModel
			await _gameModel.Initialize();
			_gameViewModel.Initialize();
		}
	}
}
