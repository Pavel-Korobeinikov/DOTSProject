using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace View.Components
{
	public class ButtonView : BaseView
	{
		public event Action ButtonClicked;
		
		[SerializeField] private Button _button = default;

		protected override void Subscribe()
		{
			_button.onClick.AddListener(OnButtonClicked);
		}

		protected override async UniTask Activate()
		{
			_button.interactable = true;

			await Task.CompletedTask;
		}

		protected override async UniTask Deactivate()
		{
			_button.interactable = false;

			await Task.CompletedTask;
		}

		protected override void Unsubscribe()
		{
			_button.onClick.RemoveListener(OnButtonClicked);
		}

		private void OnButtonClicked()
		{
			ButtonClicked?.Invoke();
		}
	}
}