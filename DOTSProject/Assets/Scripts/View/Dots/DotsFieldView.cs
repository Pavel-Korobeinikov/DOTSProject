using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ViewModel.Dots;

namespace View.Dots
{
	public class DotsFieldView : BaseView<DotsFieldViewModel>, IPointerDownHandler, IPointerUpHandler
	{
		[SerializeField] private GridLayoutGroup _gridLayout = default;
		[SerializeField] private Transform _dotsPlaceholder = default;
		[SerializeField] private DotView _dotPrefab = default;

		public DotView[,] Grid { get; private set; }

		protected override void Initialize()
		{
			_gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			_gridLayout.constraintCount = ViewModel.Width;
		}

		protected override void SetChildViews()
		{
			CreateField();

			foreach (var dotView in Grid)
			{
				AddChildView(dotView);
			}
		}

		protected override void Subscribe()
		{
			ViewModel.DotKilled += OnDotKilled;
		}

		protected override void Unsubscribe()
		{
			ViewModel.DotKilled -= OnDotKilled;
		}

		private void CreateField()
		{
			for (var x = 0; x < ViewModel.Width; x++)
			{
				for (var y = 0; y < ViewModel.Height; y++)
				{
					//TODO: Use GameObject Pool
					var instantiatedView = Instantiate(_dotPrefab, _dotsPlaceholder);
					var dotViewModel = ViewModel.Grid[x, y];
					instantiatedView.SetViewModel(dotViewModel);
					Grid[x,y] = instantiatedView;
				}
			}
		}

		protected override void Utilize()
		{
			foreach (var dotView in Grid)
			{
				//TODO: Use GameObject Pool
				Destroy(dotView);
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			ViewModel.DotsPressFinished();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			// Method needed for work of OnPointerUp
		}

		private void OnDotKilled(int x, int y)
		{
			Grid[x, y].Kill();
		}
	}
}