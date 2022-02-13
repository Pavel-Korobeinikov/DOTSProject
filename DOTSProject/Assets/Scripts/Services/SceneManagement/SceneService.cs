using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Services.Configuration.Structure;
using Services.SceneManagement.SceneLoading;
using View;
using View.Scenes;
using ViewModel;

namespace Services.SceneManagement
{
	public class SceneService : ISceneService
	{
		private readonly IGameViewModel _gameViewModel;
		private readonly SceneLoader _sceneLoader;
		private readonly Dictionary<string, BaseView> _activeScenes = new Dictionary<string, BaseView>();
		
		private readonly List<UniTask> _taskLoadCache = new List<UniTask>();

		public SceneService(IGameViewModel gameViewModel)
		{
			_gameViewModel = gameViewModel;
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
			_taskLoadCache.Clear();
			foreach (var activeScenePair in _activeScenes)
			{
				activeScenePair.Value.UnsubscribeWithChildViews();
				var deactivateTask = activeScenePair.Value.DeactivateWithChildViews();
				_taskLoadCache.Add(deactivateTask);
			}

			await UniTask.WhenAll(_taskLoadCache);
			
			_taskLoadCache.Clear();
			foreach (var activeScenePair in _activeScenes)
			{
				activeScenePair.Value.UtilizeWithChildViews();
				var unloadScene = _sceneLoader.UnloadScene(activeScenePair.Key);
				_taskLoadCache.Add(unloadScene);
			}

			await UniTask.WhenAll(_taskLoadCache);
			
			_activeScenes.Clear();
		}

		private async UniTask LoadSceneWithDependencies(SceneEntity sceneEntity)
		{
			var loadingSceneTasks = new List<UniTask>();
			var loadedScene = await _sceneLoader.LoadScene(sceneEntity);
			foreach (var dependencyScene in sceneEntity.SceneDependencies)
			{
				if (_activeScenes.Select(scenePair => scenePair.Key)
					.Contains(dependencyScene.ScenePath))
				{
					continue;
				}

				loadingSceneTasks.Add(LoadSceneWithDependencies(dependencyScene));
			}

			await UniTask.WhenAll(loadingSceneTasks);
			
			var scenePath = loadedScene.Item1;
			var viewModel = loadedScene.Item2;
			_activeScenes.Add(scenePath, viewModel);
			
			if (viewModel is ISceneView sceneView)
			{
				sceneView.SetViewModel(_gameViewModel);
			}

			viewModel.InitializeWithChildViews();
			viewModel.SetDependencies();
			viewModel.SubscribeWithChildViews();
			
			_taskLoadCache.Clear();
			var activateTask = viewModel.ActivateWithChildViews();
			_taskLoadCache.Add(activateTask);
			
			await UniTask.WhenAll(_taskLoadCache);
		}
	}
}