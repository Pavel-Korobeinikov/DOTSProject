using Configuration.Structure.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ViewModel.SceneManagement
{
	public class SceneLoader
	{
		public async UniTask<SceneBase> LoadScene(SceneEntity sceneEntity)
		{
			await UnitySceneManager.LoadSceneAsync(sceneEntity.ScenePath, LoadSceneMode.Additive);
			
			return GetSceneBaseComponent(sceneEntity);
		}

		public async UniTask UnloadScene(SceneEntity sceneEntity)
		{
			await UnitySceneManager.UnloadSceneAsync(sceneEntity.ScenePath);
		}

		private SceneBase GetSceneBaseComponent(SceneEntity sceneEntity)
		{
			var scene = UnitySceneManager.GetSceneByPath(sceneEntity.ScenePath);
			var rootGameObject = scene.GetRootGameObjects()[0];
			var sceneBase = rootGameObject.GetComponent<SceneBase>();
			sceneBase.Entity = sceneEntity;
			
			return sceneBase;
		}
	}
}