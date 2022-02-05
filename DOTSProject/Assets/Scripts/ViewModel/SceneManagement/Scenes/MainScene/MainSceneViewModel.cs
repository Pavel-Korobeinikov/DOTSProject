using Configuration.Structure.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ViewModel.SceneManagement.Scenes.MainScene
{
	public class MainSceneViewModel : MonoBehaviour, ISceneViewModel
	{
		public SceneEntity Entity { get; set; }
		
		public void Initialize()
		{
			Debug.Log("Initialized");
		}

		public UniTask Activate()
		{
			Debug.Log("Activated");
			return UniTask.CompletedTask;
		}

		public UniTask Deactivate()
		{
			Debug.Log("Deactivated");
			return UniTask.CompletedTask;
		}

		public void Utilize()
		{
			Debug.Log("Utilized");
		}
	}
}