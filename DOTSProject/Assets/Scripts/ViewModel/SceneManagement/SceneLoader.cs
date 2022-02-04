using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Scene = Configuration.Structure.Scenes.Scene;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ViewModel.SceneManagement
{
	public class SceneLoader
	{
		public async UniTask LoadScene(Scene scene)
		{
			await UnitySceneManager.LoadSceneAsync(scene.AssetPath, LoadSceneMode.Additive);
		}

		public async UniTask UnloadScene(Scene scene)
		{
			await UnitySceneManager.UnloadSceneAsync(scene.AssetPath);
		}
	}
}