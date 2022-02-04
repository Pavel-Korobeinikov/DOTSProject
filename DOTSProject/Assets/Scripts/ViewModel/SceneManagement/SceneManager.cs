using Application.MessageLog;
using Cysharp.Threading.Tasks;
using Scene = Configuration.Structure.Scenes.Scene;

namespace ViewModel.SceneManagement
{
	public class SceneManager
	{
		private SceneLoader _sceneLoader;
		private bool _loadingInProcess;

		public SceneManager()
		{
			_sceneLoader = new SceneLoader();
		}

		public async UniTask ChangeScene(Scene scene, Scene loadingScene)
		{
			if (_loadingInProcess)
			{
				MessageLogger.LogError("Loading scene already in progress.");
			}

			_loadingInProcess = true;
			
			await _sceneLoader.LoadScene(loadingScene);
			await _sceneLoader.LoadScene(scene);
			await _sceneLoader.UnloadScene(loadingScene);
			
			_loadingInProcess = false;
		}
	}
}