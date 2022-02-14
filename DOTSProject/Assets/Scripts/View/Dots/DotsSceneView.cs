using Application.MessageLog;
using Cysharp.Threading.Tasks;
using UnityEngine;
using View.Scenes;
using ViewModel.Dots;

namespace View.Dots
{
	public class DotsSceneView : SceneView<DotsSceneViewModel>
	{
		[SerializeField] private DotsFieldView _fieldView = default;
		[SerializeField] private DotsConnectionAggregatorView _connectionAggregatorView = default;
		
		protected override void SetChildViews()
		{
			_fieldView.SetViewModel(ViewModel.FieldViewModel);
			_connectionAggregatorView.SetViewModel(ViewModel.ConnectionAggregatorViewModel);
			_connectionAggregatorView.Connect(_fieldView);
			
			UniTask.Action(() => AddChildView(_fieldView)).Invoke();
			UniTask.Action(() => AddChildView(_connectionAggregatorView)).Invoke();
		}

		protected override void Initialize()
		{
			ViewModel.LaunchGame();
		}

		protected override UniTask Activate()
		{
			MessageLogger.Log("Battle Scene Activated");
			
			//TODO: Implement animation transition between scenes

			return base.Activate();
		}
		
		protected override UniTask Deactivate()
		{
			MessageLogger.Log("Battle Scene Deactivated");
			
			//TODO: Implement animation transition between scenes
			
			return base.Deactivate();
		}
	}
}