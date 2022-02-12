using System.Linq;
using DotsCore;
using Model;
using Services;
using Services.Configuration;
using Random = UnityEngine.Random;

namespace ViewModel.Scenes
{
	public class BattleSceneViewModel : BaseViewModel
	{
		private DotsGame _gameCore;
		
		public override void Initialize(GameModel gameModel, IServiceManager serviceManager)
		{
			base.Initialize(gameModel, serviceManager);

			InitializeBattle();
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
		}

		private void LaunchBattle()
		{
			_gameCore.Launch();
		}
	}
}