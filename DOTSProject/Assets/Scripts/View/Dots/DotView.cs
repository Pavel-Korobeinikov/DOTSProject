using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ViewModel.Dots;

namespace View.Dots
{
	public class DotView : BaseView<DotViewModel>, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
	{
		[SerializeField] private Image _image = default;

		private Vector2 _preferredPosition;
		
		protected override void Initialize()
		{
			_image.color = new Color(ViewModel.R, ViewModel.G, ViewModel.B);
		}

		public void SetPreferredPosition(Vector2 position)
		{
			_preferredPosition = position;

			GetComponent<RectTransform>().anchoredPosition = _preferredPosition;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			// Method needed for work of OnPointerUp
		}
		
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
			{
				ViewModel.Press();
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			ViewModel.FinishPress();
		}

		public void Kill()
		{
			// TODO: Play kill animation
		}
	}
}