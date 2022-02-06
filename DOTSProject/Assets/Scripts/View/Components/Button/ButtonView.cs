using UnityEngine;

namespace View.Components.Button
{
	public class ButtonView : MonoBehaviour
	{
		[SerializeField] private UnityEngine.UI.Button _button = default;

		public void SetInteractive(bool interactive)
		{
			_button.interactable = interactive;
		}
	}
}