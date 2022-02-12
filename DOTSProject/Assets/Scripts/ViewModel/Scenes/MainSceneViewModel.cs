using Cysharp.Threading.Tasks;
using Services.Configuration;
using Services.SceneManagement;

namespace ViewModel.Scenes
{
	public class MainSceneViewModel : BaseViewModel
	{
		public async UniTask ActivateBattleScene()
		{
			var sceneService = _serviceManager.GetService<ISceneService>();
			var configurationService = _serviceManager.GetService<IConfigurationService>();
			var battleScene = configurationService.GameConfiguration.BattleScene;

			await sceneService.ActivateScene(battleScene, ActivationSceneMode.Single);
		}
	}
}