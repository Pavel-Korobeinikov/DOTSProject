using Configuration.Structure.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using ViewModel.SceneManagement.Scenes;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ViewModel.SceneManagement.SceneLoading
{
	public class SceneLoader
	{
		public async UniTask<SceneViewModel> LoadScene(SceneEntity sceneEntity)
		{
			await UnitySceneManager.LoadSceneAsync(sceneEntity.ScenePath, LoadSceneMode.Additive);
			
			return GetSceneBaseComponent(sceneEntity);
		}

		public async UniTask UnloadScene(SceneEntity sceneEntity)
		{
			await UnitySceneManager.UnloadSceneAsync(sceneEntity.ScenePath);
		}

		private SceneViewModel GetSceneBaseComponent(SceneEntity sceneEntity)
		{
			var scene = UnitySceneManager.GetSceneByPath(sceneEntity.ScenePath);
			var rootGameObject = scene.GetRootGameObjects()[0];
			var sceneBase = rootGameObject.GetComponent<SceneViewModel>();
			sceneBase.Entity = sceneEntity;
			
			return sceneBase;
		}
	}
}