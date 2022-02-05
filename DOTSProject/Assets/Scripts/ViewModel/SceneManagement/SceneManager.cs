using System.Collections.Generic;
using System.Linq;
using Configuration.Structure.Scenes;
using Cysharp.Threading.Tasks;
using ViewModel.SceneManagement.Scenes;
using ViewModel.SceneManagement.SceneLoading;

namespace ViewModel.SceneManagement
{
	public class SceneManager
	{
		private readonly SceneLoader _sceneLoader;
		private readonly List<ISceneViewModel> _activeScenes = new List<ISceneViewModel>();
		private readonly List<UniTask> _taskCache = new List<UniTask>();
		private readonly List<UniTask<ISceneViewModel>> _taskSceneCache = new List<UniTask<ISceneViewModel>>();

		public SceneManager()
		{
			_sceneLoader = new SceneLoader();
		}

		public async UniTask ActivateScene(SceneEntity sceneEntity, ActivationSceneMode activationMode)
		{
			if (activationMode == ActivationSceneMode.Single)
			{
				await UtilizeCurrentScenes();
			}
			
			await LoadSceneWithDependencies(sceneEntity);
		}

		private async UniTask UtilizeCurrentScenes()
		{
			_taskCache.Clear();
			foreach (var activeScene in _activeScenes)
			{
				var deactivateTask = activeScene.Deactivate();
				_taskCache.Add(deactivateTask);
			}

			await UniTask.WhenAll(_taskCache);
			
			_taskCache.Clear();
			foreach (var activeScene in _activeScenes)
			{
				activeScene.Utilize();
				var unloadScene = _sceneLoader.UnloadScene(activeScene.Entity);
				_taskCache.Add(unloadScene);
			}

			await UniTask.WhenAll(_taskCache);
			
			_activeScenes.Clear();
		}

		private async UniTask LoadSceneWithDependencies(SceneEntity sceneEntity)
		{
			_taskSceneCache.Clear();
			_taskSceneCache.Add(_sceneLoader.LoadScene(sceneEntity));
			foreach (var dependencyScene in sceneEntity.SceneDependencies)
			{
				if (_activeScenes.Select(scene => scene.Entity.ScenePath)
					.Contains(dependencyScene.ScenePath))
				{
					continue;
				}

				var sceneBasesUniTask = _sceneLoader.LoadScene(dependencyScene);
				_taskSceneCache.Add(sceneBasesUniTask);
			}

			var loadedScenes = await UniTask.WhenAll(_taskSceneCache);
			
			foreach (var scene in loadedScenes)
			{
				_activeScenes.Add(scene);
			}
			
			_taskCache.Clear();
			foreach (var activeScene in _activeScenes)
			{
				activeScene.Initialize();
				var activateTask = activeScene.Activate();
				_taskCache.Add(activateTask);
			}
			
			await UniTask.WhenAll(_taskCache);
		}
	}
}