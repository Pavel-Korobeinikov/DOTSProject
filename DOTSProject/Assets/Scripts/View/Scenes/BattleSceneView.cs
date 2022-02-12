using Application.MessageLog;
using Cysharp.Threading.Tasks;
using ViewModel.Scenes;

namespace View.Scenes
{
	public class BattleSceneView : SceneView<BattleSceneViewModel>
	{
		protected override UniTask Activate()
		{
			MessageLogger.Log("Battle Scene Activated");
			
			return base.Activate();
		}
		
		protected override UniTask Deactivate()
		{
			MessageLogger.Log("Battle Scene Deactivated");
			
			return base.Deactivate();
		}
	}
}