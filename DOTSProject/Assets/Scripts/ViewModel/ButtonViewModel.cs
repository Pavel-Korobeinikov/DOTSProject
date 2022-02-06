using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using View.Components.Button;

namespace ViewModel
{
	public class ButtonViewModel : BaseViewModel
	{
		[SerializeField] private ButtonView _button = default;

		protected override async UniTask Activate()
		{
			_button.SetInteractive(true);

			await Task.CompletedTask;
		}

		protected override async UniTask Deactivate()
		{
			_button.SetInteractive(false);

			await Task.CompletedTask;
		}
	}
}