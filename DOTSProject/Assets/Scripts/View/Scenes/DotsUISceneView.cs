using Cysharp.Threading.Tasks;
using UnityEngine;
using View.Components;
using ViewModel.Scenes;

namespace View.Scenes
{
	public class DotsUISceneView : SceneView<DotsSceneViewModel>
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

		protected override void Unsubscribe()
		{
			startEndlessModeButtonView.ButtonClicked -= OnStartEndlessModeButtonClicked;
		}

		private void OnStartEndlessModeButtonClicked()
		{
			UniTask.Action(async () => await ViewModel.ReturnToMainMenu()).Invoke();
		}
	}
}