using Configuration.Structure.Scenes;
using Cysharp.Threading.Tasks;

namespace Services.SceneManagement
{
	public interface ISceneService : IService
	{
		UniTask ActivateScene(SceneEntity sceneEntity, ActivationSceneMode activationMode);
	}
}