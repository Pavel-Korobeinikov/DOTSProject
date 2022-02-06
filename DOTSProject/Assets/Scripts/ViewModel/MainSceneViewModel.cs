using Application.MessageLog;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ViewModel
{
	public class MainSceneViewModel : BaseViewModel
	{
		[SerializeField] private BaseViewModel _startEndlessModeButtonViewModel = default;

		protected override void SetChildViewModels()
		{
			AddChildViewModel(_startEndlessModeButtonViewModel);
		}

		protected override void Initialize()
		{
			MessageLogger.Log("Initialized");
		}

		protected override async UniTask Activate()
		{
			MessageLogger.Log("Activated");
		}

		protected override async UniTask Deactivate()
		{
			MessageLogger.Log("Deactivated");
		}

		protected override void Utilize()
		{
			MessageLogger.Log("Utilized");
		}
	}
}