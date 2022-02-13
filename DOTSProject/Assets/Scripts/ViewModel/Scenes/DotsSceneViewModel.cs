using System.Linq;
using Application.MessageLog;
using Cysharp.Threading.Tasks;
using DotsCore;
using DotsCore.Events;
using Model;
using Services;
using Services.Configuration;
using Services.SceneManagement;
using ViewModel.Dots;
using Random = UnityEngine.Random;

namespace ViewModel.Scenes
{
	public class DotsSceneViewModel : BaseViewModel
	{
		public DotsFieldViewModel FieldViewModel { get; private set; }

		private DotsGame _gameCore;
		
		public override void Initialize(GameModel gameModel, IServiceManager serviceManager)
		{
			base.Initialize(gameModel, serviceManager);

			InitializeChildViewModels();
			InitializeBattle();
		}

		public void InitializeBattle()
		{
			var battleConfiguration = _serviceManager.GetService<IConfigurationService>()
				.GameConfiguration
				.BattleConfiguration;
			
			// Can be replaced by concrete seed for repeat game
			var seed = Random.Range(0, int.MaxValue);
			var colors = battleConfiguration.Dots.Select(dot => new Color(dot.Color.Name)).ToList();
			var initializationData = new DotsGameInitializationData(seed,
				battleConfiguration.Width,
				battleConfiguration.Height,
				colors);
			
			_gameCore = new DotsGame(initializationData);
			_gameCore.SubscribeOutputPort(CoreEventsHandler);
			_gameCore.Launch();
		}

		public async UniTask ReturnToMainMenu()
		{
			var sceneService = _serviceManager.GetService<ISceneService>();
			var configurationService = _serviceManager.GetService<IConfigurationService>();
			var battleScene = configurationService.GameConfiguration.MainScene;

			await sceneService.ActivateScene(battleScene, ActivationSceneMode.Single);
		}

		private void InitializeChildViewModels()
		{
			FieldViewModel = CreateViewModel<DotsFieldViewModel>();
		}

		private void CoreEventsHandler(ICoreEvent coreEvent)
		{
			MessageLogger.Log($"Event Received: {coreEvent}");
			
			var eventHandler = GameCoreEventsHandlerFactory.GetEventHandler(coreEvent, FieldViewModel);
			eventHandler.Handle();
		}
	}
}