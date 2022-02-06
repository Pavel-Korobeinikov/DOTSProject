using Application.MessageLog;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ViewModel
{
	public class MainSceneViewModel : MonoBehaviour, IViewModel
	{
		public void Initialize()
		{
			MessageLogger.Log("Initialized");
		}

		public UniTask Activate()
		{
			MessageLogger.Log("Activated");
			
			return UniTask.CompletedTask;
		}

		public UniTask Deactivate()
		{
			MessageLogger.Log("Deactivated");
			
			return UniTask.CompletedTask;
		}

		public void Utilize()
		{
			MessageLogger.Log("Utilized");
		}
	}
}