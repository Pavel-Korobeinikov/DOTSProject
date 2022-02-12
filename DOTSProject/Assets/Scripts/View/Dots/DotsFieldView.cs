using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ViewModel.Dots;

namespace View.Dots
{
	public class DotsFieldView : BaseView<DotsFieldViewModel>
	{
		[SerializeField] private GridLayoutGroup _gridLayout = default;
		[SerializeField] private Transform _dotsPlaceholder = default;
		[SerializeField] private DotView _dotPrefab = default;

		private List<DotView> _dotViews = new List<DotView>();

		protected override void Initialize()
		{
			_gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			_gridLayout.constraintCount = ViewModel.Width;
		}

		protected override void SetChildViews()
		{
			CreateField();

			foreach (var dotView in _dotViews)
			{
				AddChildView(dotView);
			}
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
					_dotViews.Add(instantiatedView);
				}
			}
		}

		protected override void Utilize()
		{
			foreach (var dotView in _dotViews)
			{
				//TODO: Use GameObject Pool
				Destroy(dotView);
			}
		}
	}
}