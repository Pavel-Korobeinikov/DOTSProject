using UnityEngine;
using UnityEngine.UI;
using ViewModel.Dots;

namespace View.Dots
{
	public class DotView : BaseView<DotViewModel>
	{
		[SerializeField] private Image _image = default;
		
		protected override void Initialize()
		{
			_image.color = new Color(ViewModel.R, ViewModel.G, ViewModel.B);
		}
	}
}