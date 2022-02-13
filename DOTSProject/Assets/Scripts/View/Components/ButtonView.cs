using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace View.Components
{
	public class ButtonView : BaseView
	{
		public event Action ButtonClicked;
		
		[SerializeField] private Button _button = default;

		protected override async UniTask Activate()
		{
			_button.onClick.AddListener(OnButtonClicked);
			_button.interactable = true;

			await UniTask.CompletedTask;
		}

		protected override async UniTask Deactivate()
		{
			_button.interactable = false;
			_button.onClick.RemoveListener(OnButtonClicked);

			await UniTask.CompletedTask;
		}

		private void OnButtonClicked()
		{
			ButtonClicked?.Invoke();
		}
	}
}