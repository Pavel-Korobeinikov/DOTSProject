using Configuration.Structure.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using ViewModel;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Services.SceneManagement.SceneLoading
{
	public class SceneLoader
	{
		public async UniTask<(string, IViewModel)> LoadScene(SceneEntity sceneEntity)
		{
			await UnitySceneManager.LoadSceneAsync(sceneEntity.ScenePath, LoadSceneMode.Additive);
			
			return (sceneEntity.ScenePath, GetSceneBaseViewModel(sceneEntity));
		}

		public async UniTask UnloadScene(string scenePath)
		{
			await UnitySceneManager.UnloadSceneAsync(scenePath);
		}

		private IViewModel GetSceneBaseViewModel(SceneEntity sceneEntity)
		{
			var scene = UnitySceneManager.GetSceneByPath(sceneEntity.ScenePath);
			var rootGameObject = scene.GetRootGameObjects()[0];
			var sceneViewModel = rootGameObject.GetComponent<IViewModel>();
			
			return sceneViewModel;
		}
	}
}