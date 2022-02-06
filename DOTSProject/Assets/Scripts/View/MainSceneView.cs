using Application.MessageLog;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ViewModel;

namespace View
{
	public class MainSceneView : SceneView<MainSceneViewModel>
	{
		[SerializeField] private ButtonView startEndlessModeButtonView = default;

		protected override void SetChildViews()
		{
			AddChildView(startEndlessModeButtonView);
		}

		protected override void Subscribe()
		{
			startEndlessModeButtonView.ButtonClicked += OnStartEndlessModeButtonClicked;
		}

		protected override UniTask Activate()
		{
			MessageLogger.Log("Main Scene Activated");
			
			return base.Activate();
		}

		protected override UniTask Deactivate()
		{
			MessageLogger.Log("Main Scene Deactivated");

			return base.Deactivate();
		}

		protected override void Unsubscribe()
		{
			startEndlessModeButtonView.ButtonClicked -= OnStartEndlessModeButtonClicked;
		}

		private void OnStartEndlessModeButtonClicked()
		{
			UniTask.Action(async () => await ViewModel.ActivateBattleScene()).Invoke();
		}
	}
}