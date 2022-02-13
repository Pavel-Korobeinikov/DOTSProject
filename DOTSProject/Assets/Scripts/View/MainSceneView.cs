using Application.MessageLog;
using Cysharp.Threading.Tasks;
using UnityEngine;
using View.Components;
using ViewModel;

namespace View.Scenes
{
	public class MainSceneView : SceneView<MainSceneViewModel>
	{
		[SerializeField] private ButtonView startEndlessModeButtonView = default;

		protected override void SetChildViews()
		{
			UniTask.Action(() => AddChildView(startEndlessModeButtonView)).Invoke();
		}

		protected override UniTask Activate()
		{
			MessageLogger.Log("Main Scene Activated");
			
			startEndlessModeButtonView.ButtonClicked += OnStartEndlessModeButtonClicked;
			
			//TODO: Implement animation transition between scenes
			
			return base.Activate();
		}

		protected override UniTask Deactivate()
		{
			MessageLogger.Log("Main Scene Deactivated");
			
			startEndlessModeButtonView.ButtonClicked -= OnStartEndlessModeButtonClicked;
			
			//TODO: Implement animation transition between scenes

			return base.Deactivate();
		}

		private void OnStartEndlessModeButtonClicked()
		{
			UniTask.Action(async () => await ViewModel.ActivateBattleScene()).Invoke();
		}
	}
}