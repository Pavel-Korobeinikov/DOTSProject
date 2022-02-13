using System.Linq;
using Application.MessageLog;
using Cysharp.Threading.Tasks;
using DotsCore;
using DotsCore.Events;
using Services.Configuration;
using Services.SceneManagement;
using Random = UnityEngine.Random;

namespace ViewModel.Dots
{
	public class DotsSceneViewModel : BaseViewModel
	{
		public DotsFieldViewModel FieldViewModel { get; private set; }

		private DotsGame _gameCore;
		private CoreInputDispatcher _inputDispatcher;

		public void LaunchGame()
		{
			InitializeGameCore();
			InitializeChildViewModels();
			LaunchGameCore();
		}

		public async UniTask ReturnToMainMenu()
		{
			var sceneService = ServiceManager.GetService<ISceneService>();
			var configurationService = ServiceManager.GetService<IConfigurationService>();
			var battleScene = configurationService.GameConfiguration.MainScene;

			await sceneService.ActivateScene(battleScene, ActivationSceneMode.Single);
		}

		private void InitializeGameCore()
		{
			var battleConfiguration = ServiceManager.GetService<IConfigurationService>()
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
			_inputDispatcher = new CoreInputDispatcher(_gameCore);
			_gameCore.SubscribeOutputPort(CoreEventsHandler);
		}

		private void InitializeChildViewModels()
		{
			FieldViewModel = CreateViewModel<DotsFieldViewModel>();
			FieldViewModel.SetCoreInputDispatcher(_inputDispatcher);
		}

		private void LaunchGameCore()
		{
			_gameCore.Launch();
		}

		private void CoreEventsHandler(ICoreEvent coreEvent)
		{
			MessageLogger.Log($"Event Received: {coreEvent}");
			
			var eventHandler = GameCoreEventsHandlerFactory.GetEventHandler(coreEvent, FieldViewModel);
			eventHandler.Handle();
		}
	}
}