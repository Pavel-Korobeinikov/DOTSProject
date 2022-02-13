using Cysharp.Threading.Tasks;
using UnityEngine;
using View.Components;
using ViewModel.Dots;

namespace View.Dots
{
	public class DotsUISceneView : SceneView<DotsSceneViewModel>
	{
		[SerializeField] private ButtonView startEndlessModeButtonView = default;

		protected override void SetChildViews()
		{
			UniTask.Action(() => AddChildView(startEndlessModeButtonView)).Invoke();
		}
		
		protected override async UniTask Activate()
		{
			startEndlessModeButtonView.ButtonClicked += OnStartEndlessModeButtonClicked;

			await UniTask.CompletedTask;
		}

		protected override async UniTask Deactivate()
		{
			startEndlessModeButtonView.ButtonClicked -= OnStartEndlessModeButtonClicked;

			await UniTask.CompletedTask;
		}

		private void OnStartEndlessModeButtonClicked()
		{
			UniTask.Action(async () => await ViewModel.ReturnToMainMenu()).Invoke();
		}
	}
}