using Application.MessageLog;
using Cysharp.Threading.Tasks;
using ViewModel.Scenes.Dots;

namespace View.Scenes.Dots
{
	public class DotsSceneView : SceneView<DotsSceneViewModel>
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