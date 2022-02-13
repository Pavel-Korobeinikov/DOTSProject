﻿using Application.MessageLog;
using Cysharp.Threading.Tasks;
using UnityEngine;
using View.Dots;
using ViewModel.Scenes;

namespace View.Scenes
{
	public class DotsSceneView : SceneView<DotsSceneViewModel>
	{
		[SerializeField] private DotsFieldView _dotsFieldView = default;
		
		protected override void SetChildViews()
		{
			_dotsFieldView.SetViewModel(ViewModel.FieldViewModel);
			
			AddChildView(_dotsFieldView);
		}

		protected override void Initialize()
		{
			ViewModel.InitializeBattle();
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