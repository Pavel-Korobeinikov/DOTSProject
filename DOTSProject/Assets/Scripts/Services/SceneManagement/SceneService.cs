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
		
		private readonly List<UniTask> _taskCache = new List<UniTask>();
		private readonly List<UniTask> _taskLoadCache = new List<UniTask>();
		private readonly List<UniTask<(string, BaseView)>> _taskLoadSceneCache = new List<UniTask<(string, BaseView)>>();

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
			
			_taskLoadSceneCache.Clear();
			_taskLoadCache.Clear();
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
			foreach (var dependencyScene in sceneEntity.SceneDependencies)
			{
				if (_activeScenes.Select(scenePair => scenePair.Key)
					.Contains(dependencyScene.ScenePath))
				{
					continue;
				}

				_taskLoadCache.Add(LoadSceneWithDependencies(sceneEntity));
			}
			_taskLoadSceneCache.Add(_sceneLoader.LoadScene(sceneEntity));

			await UniTask.WhenAll(_taskLoadCache);
			var loadedScenes = await UniTask.WhenAll(_taskLoadSceneCache);
			
			_taskCache.Clear();
			foreach (var (scenePath, viewModel) in loadedScenes)
			{
				_activeScenes.Add(scenePath, viewModel);
				
				if (viewModel is ISceneView sceneView)
				{
					sceneView.SetViewModel(_gameViewModel);
				}

				viewModel.SetDependencies();
				viewModel.InitializeWithChildViews();
				viewModel.SubscribeWithChildViews();
				var activateTask = viewModel.ActivateWithChildViews();
				_taskCache.Add(activateTask);
			}
			
			await UniTask.WhenAll(_taskCache);
		}
	}
}