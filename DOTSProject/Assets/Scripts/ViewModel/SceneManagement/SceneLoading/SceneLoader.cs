using Configuration.Structure.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using ViewModel.SceneManagement.Scenes;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ViewModel.SceneManagement.SceneLoading
{
	public class SceneLoader
	{
		public async UniTask<ISceneViewModel> LoadScene(SceneEntity sceneEntity)
		{
			await UnitySceneManager.LoadSceneAsync(sceneEntity.ScenePath, LoadSceneMode.Additive);
			
			return GetSceneBaseViewModel(sceneEntity);
		}

		public async UniTask UnloadScene(SceneEntity sceneEntity)
		{
			await UnitySceneManager.UnloadSceneAsync(sceneEntity.ScenePath);
		}

		private ISceneViewModel GetSceneBaseViewModel(SceneEntity sceneEntity)
		{
			var scene = UnitySceneManager.GetSceneByPath(sceneEntity.ScenePath);
			var rootGameObject = scene.GetRootGameObjects()[0];
			var sceneViewModel = rootGameObject.GetComponent<ISceneViewModel>();
			
			return sceneViewModel;
		}
	}
}