using System.Linq;
using Application.MessageLog;
using DotsCore;
using DotsCore.Events;
using Model;
using Services;
using Services.Configuration;
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

			InitializeBattle();
			InitializeChildViewModels();
			LaunchBattle();
		}

		private void InitializeBattle()
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

		private void LaunchBattle()
		{
			_gameCore.Launch();
		}
	}
}