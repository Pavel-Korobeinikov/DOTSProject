﻿using System.Collections.Generic;
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
		private readonly Dictionary<string, IViewModel> _activeScenes = new Dictionary<string, IViewModel>();
		private readonly List<UniTask> _taskCache = new List<UniTask>();
		private readonly List<UniTask<(string, IViewModel)>> _taskSceneCache = new List<UniTask<(string, IViewModel)>>();

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
			
			await LoadSceneWithDependencies(sceneEntity);
		}

		private async UniTask UtilizeCurrentScenes()
		{
			_taskCache.Clear();
			foreach (var activeScenePair in _activeScenes)
			{
				var deactivateTask = activeScenePair.Value.Deactivate();
				_taskCache.Add(deactivateTask);
			}

			await UniTask.WhenAll(_taskCache);
			
			_taskCache.Clear();
			foreach (var activeScenePair in _activeScenes)
			{
				activeScenePair.Value.Utilize();
				var unloadScene = _sceneLoader.UnloadScene(activeScenePair.Key);
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
				if (_activeScenes.Select(scenePair => scenePair.Key)
					.Contains(dependencyScene.ScenePath))
				{
					continue;
				}

				var sceneBasesUniTask = _sceneLoader.LoadScene(dependencyScene);
				_taskSceneCache.Add(sceneBasesUniTask);
			}

			var loadedScenes = await UniTask.WhenAll(_taskSceneCache);
			
			foreach (var (scenePath, viewModel) in loadedScenes)
			{
				_activeScenes.Add(scenePath, viewModel);
			}
			
			_taskCache.Clear();
			foreach (var activeScenePair in _activeScenes)
			{
				activeScenePair.Value.Initialize();
				var activateTask = activeScenePair.Value.Activate();
				_taskCache.Add(activateTask);
			}
			
			await UniTask.WhenAll(_taskCache);
		}
	}
}