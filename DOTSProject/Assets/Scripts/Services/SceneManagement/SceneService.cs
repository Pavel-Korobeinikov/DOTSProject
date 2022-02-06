using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Services.Configuration.Structure.Scenes;
using Services.SceneManagement.SceneLoading;
using ViewModel;

namespace Services.SceneManagement
{
	public class SceneService : ISceneService
	{
		private readonly SceneLoader _sceneLoader;
		private readonly Dictionary<string, BaseViewModel> _activeScenes = new Dictionary<string, BaseViewModel>();
		private readonly List<UniTask> _taskCache = new List<UniTask>();
		private readonly List<UniTask> _taskLoadCache = new List<UniTask>();
		private readonly List<UniTask<(string, BaseViewModel)>> _taskLoadSceneCache = new List<UniTask<(string, BaseViewModel)>>();

		public SceneService()
		{
			_sceneLoader = new SceneLoader();
		}

		public async UniTask ActivateScene(SceneEntity sceneEntity, ActivationSceneMode activationMode)
		{
			if (activationMode == ActivationSceneMode.Single)
			{
				await UtilizeCurrentScenes();
			}
			
			_taskLoadSceneCache.Clear();
			_taskLoadCache.Clear();
			await LoadSceneWithDependencies(sceneEntity);
		}

		private async UniTask UtilizeCurrentScenes()
		{
			_taskLoadCache.Clear();
			foreach (var activeScenePair in _activeScenes)
			{
				activeScenePair.Value.UnsubscribeWithChildViewModels();
				var deactivateTask = activeScenePair.Value.DeactivateWithChildViewModels();
				_taskLoadCache.Add(deactivateTask);
			}

			await UniTask.WhenAll(_taskLoadCache);
			
			_taskLoadCache.Clear();
			foreach (var activeScenePair in _activeScenes)
			{
				activeScenePair.Value.UtilizeWithChildViewModels();
				var unloadScene = _sceneLoader.UnloadScene(activeScenePair.Key);
				_taskLoadCache.Add(unloadScene);
			}

			await UniTask.WhenAll(_taskLoadCache);
			
			_activeScenes.Clear();
		}

		private async UniTask LoadSceneWithDependencies(SceneEntity sceneEntity)
		{
			_taskLoadSceneCache.Add(_sceneLoader.LoadScene(sceneEntity));
			foreach (var dependencyScene in sceneEntity.SceneDependencies)
			{
				if (_activeScenes.Select(scenePair => scenePair.Key)
					.Contains(dependencyScene.ScenePath))
				{
					continue;
				}

				_taskLoadCache.Add(LoadSceneWithDependencies(sceneEntity));
			}

			await UniTask.WhenAll(_taskLoadCache);
			var loadedScenes = await UniTask.WhenAll(_taskLoadSceneCache);
			
			foreach (var (scenePath, viewModel) in loadedScenes)
			{
				_activeScenes.Add(scenePath, viewModel);
			}
			
			_taskCache.Clear();
			foreach (var activeScenePair in _activeScenes)
			{
				activeScenePair.Value.SetDependencies();
				activeScenePair.Value.InitializeWithChildViewModels();
				activeScenePair.Value.SubscribeWithChildViewModels();
				var activateTask = activeScenePair.Value.ActivateWithChildViewModels();
				_taskCache.Add(activateTask);
			}
			
			await UniTask.WhenAll(_taskCache);
		}
	}
}