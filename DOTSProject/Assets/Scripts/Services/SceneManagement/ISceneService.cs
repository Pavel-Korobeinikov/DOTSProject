using Cysharp.Threading.Tasks;
using Services.Configuration.Structure.Scenes;

namespace Services.SceneManagement
{
	public interface ISceneService : IService
	{
		UniTask ActivateScene(SceneEntity sceneEntity, ActivationSceneMode activationMode);
	}
}