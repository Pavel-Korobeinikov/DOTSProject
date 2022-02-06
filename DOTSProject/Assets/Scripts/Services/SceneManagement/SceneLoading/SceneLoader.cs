using Cysharp.Threading.Tasks;
using Services.Configuration.Structure.Scenes;
using UnityEngine.SceneManagement;
using ViewModel;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Services.SceneManagement.SceneLoading
{
	public class SceneLoader
	{
		public async UniTask<(string, BaseViewModel)> LoadScene(SceneEntity sceneEntity)
		{
			await UnitySceneManager.LoadSceneAsync(sceneEntity.ScenePath, LoadSceneMode.Additive);
			
			return (sceneEntity.ScenePath, GetSceneBaseViewModel(sceneEntity));
		}

		public async UniTask UnloadScene(string scenePath)
		{
			await UnitySceneManager.UnloadSceneAsync(scenePath);
		}

		private BaseViewModel GetSceneBaseViewModel(SceneEntity sceneEntity)
		{
			var scene = UnitySceneManager.GetSceneByPath(sceneEntity.ScenePath);
			var rootGameObject = scene.GetRootGameObjects()[0];
			var sceneViewModel = rootGameObject.GetComponent<BaseViewModel>();
			
			return sceneViewModel;
		}
	}
}