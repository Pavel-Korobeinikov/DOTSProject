using Configuration.Structure.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using ViewModel;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Services.SceneManagement.SceneLoading
{
	public class SceneLoader
	{
		public async UniTask<IViewModel> LoadScene(SceneEntity sceneEntity)
		{
			await UnitySceneManager.LoadSceneAsync(sceneEntity.ScenePath, LoadSceneMode.Additive);
			
			return GetSceneBaseViewModel(sceneEntity);
		}

		public async UniTask UnloadScene(SceneEntity sceneEntity)
		{
			await UnitySceneManager.UnloadSceneAsync(sceneEntity.ScenePath);
		}

		private IViewModel GetSceneBaseViewModel(SceneEntity sceneEntity)
		{
			var scene = UnitySceneManager.GetSceneByPath(sceneEntity.ScenePath);
			var rootGameObject = scene.GetRootGameObjects()[0];
			var sceneViewModel = rootGameObject.GetComponent<IViewModel>();
			sceneViewModel.Entity = sceneEntity;
			
			return sceneViewModel;
		}
	}
}