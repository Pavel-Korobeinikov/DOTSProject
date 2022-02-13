using System;
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
		[SerializeField] private float _delayByRowPercent = default;
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

			_moveTween = DOTween.To(
				() => _rectTransform.anchoredPosition,
				currentPosition => _rectTransform.anchoredPosition = currentPosition,
				position,
				_fallSpeed).SetEase(Ease.OutQuad).SetDelay(ViewModel.Position.Y * _delayByRowPercent);
			_moveTween.Play();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
			{
				ViewModel.IsPressed = true;
			}
		}
		
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
			{
				ViewModel.IsPressed = true;
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			ViewModel.IsPressed = false;
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