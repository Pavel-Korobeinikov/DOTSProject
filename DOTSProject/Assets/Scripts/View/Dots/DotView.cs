using Application.MessageLog;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ViewModel.Dots;
using Color = UnityEngine.Color;

namespace View.Dots
{
	public class DotView : BaseView<DotViewModel>, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
	{
		[Header("View Options")] 
		[SerializeField] private float _fallSpeed = default;
		[SerializeField] private float _dropTimeDelay = default;
		[SerializeField] private float _dropOvershoot = default;
		[Header("Dependencies")]
		[SerializeField] private Image _image = default;

		private Vector2 _preferredPosition;
		private RectTransform _rectTransform;

		private Tween _moveTween;

		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		protected override void Initialize()
		{
			_image.color = new Color(ViewModel.R, ViewModel.G, ViewModel.B);
		}

		public void SetCurrentPosition(Vector2 position)
		{
			_rectTransform.anchoredPosition = position;
		}

		public void SetPreferredPosition(Vector2 position)
		{
			_preferredPosition = position;

			var delay = _dropTimeDelay / (ViewModel.Position.Y + 1);
			_moveTween = DOTween.To(
				() => _rectTransform.anchoredPosition,
				currentPosition => _rectTransform.anchoredPosition = currentPosition,
				position,
				_fallSpeed).SetEase(Ease.OutBack, _dropOvershoot).SetDelay(delay);
			_moveTween.Play();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
			{
				ViewModel.Press();
			}
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
			ViewModel.PressFinish();
		}

		public void Kill()
		{
			// TODO: Play kill animation
		}

		protected override void Utilize()
		{
			_moveTween.Kill(true);
		}
	}
}