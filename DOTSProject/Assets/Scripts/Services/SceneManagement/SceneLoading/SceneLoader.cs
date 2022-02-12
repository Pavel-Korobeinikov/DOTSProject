using System;
using Cysharp.Threading.Tasks;
using Services.Configuration.Structure;
using UnityEngine.SceneManagement;
using View;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Services.SceneManagement.SceneLoading
{
	public class SceneLoader
	{
		public async UniTask<(string, BaseView)> LoadScene(SceneEntity sceneEntity)
		{
			await UnitySceneManager.LoadSceneAsync(sceneEntity.ScenePath, LoadSceneMode.Additive);
			
			return (sceneEntity.ScenePath, GetSceneBaseView(sceneEntity));
		}

		public async UniTask UnloadScene(string scenePath)
		{
			await UnitySceneManager.UnloadSceneAsync(scenePath);
		}

		private BaseView GetSceneBaseView(SceneEntity sceneEntity)
		{
			var scene = UnitySceneManager.GetSceneByPath(sceneEntity.ScenePath);
			var rootGameObject = scene.GetRootGameObjects()[0];
			var baseView = rootGameObject.GetComponent<BaseView>();
			if (baseView == null)
			{
				throw new Exception($"Base view in root object of {sceneEntity} doesn't found");
			}

			return baseView;
		}
	}
}