using Cysharp.Threading.Tasks;
using Services.Configuration;
using Services.SceneManagement;

namespace ViewModel
{
	public class MainSceneViewModel : BaseViewModel
	{
		public async UniTask ActivateBattleScene()
		{
			var sceneService = ServiceManager.GetService<ISceneService>();
			var configurationService = ServiceManager.GetService<IConfigurationService>();
			var battleScene = configurationService.GameConfiguration.DotsScene;

			await sceneService.ActivateScene(battleScene, ActivationSceneMode.Single);
		}
	}
}