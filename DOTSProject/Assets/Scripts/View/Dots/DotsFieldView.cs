using Cysharp.Threading.Tasks;
using DotsCore;
using UnityEngine;
using UnityEngine.EventSystems;
using ViewModel.Dots;

namespace View.Dots
{
	public class DotsFieldView : BaseView<DotsFieldViewModel>, IPointerDownHandler, IPointerUpHandler
	{
		[Header("View Options")] 
		[SerializeField] private int _dotHorizontalSpacing = default;
		[SerializeField] private int _dotVerticalSpacing = default;
		[SerializeField] private int _dotSpawnIndent = default;
		[Header("Dependencies")]
		[SerializeField] private Transform _dotsPlaceholder = default;
		[SerializeField] private DotView _dotPrefab = default;

		public DotView[,] Grid { get; set; }
		
		private float _dotWidth;
		private float _dotHeight;
		private Vector2[,] _gridPositions;


		protected override void Initialize()
		{
			var dotRectTransform = _dotPrefab.GetComponent<RectTransform>();
			var rect = dotRectTransform.rect;
			
			_dotWidth = rect.width;
			_dotHeight = rect.height;
		}

		protected override void SetChildViews()
		{
			FillFieldPositions();
			CreateFieldView();

			foreach (var dotView in Grid)
			{
				UniTask.Action(() => AddChildView(dotView)).Invoke();
			}
		}
		
		protected override async UniTask Activate()
		{
			ViewModel.GridUpdated += OnGridUpdated;
			ViewModel.DotKilled += OnDotKilled;

			await UniTask.CompletedTask;
		}

		protected override async UniTask Deactivate()
		{
			ViewModel.DotKilled -= OnDotKilled;
			ViewModel.GridUpdated -= OnGridUpdated;

			await UniTask.CompletedTask;
		}

		private void FillFieldPositions()
		{
			_gridPositions = new Vector2[ViewModel.Width, ViewModel.Height];
			
			for (var x = 0; x < ViewModel.Width; x++)
			{
				for (var y = 0; y < ViewModel.Height; y++)
				{
					var positionX = (_dotWidth / 2f) + (_dotHorizontalSpacing * x) + (_dotWidth * x);
					var positionY = -(_dotHeight / 2f) - (_dotVerticalSpacing * y) - (_dotHeight * y);
					
					_gridPositions[x, y] = new Vector2(positionX, positionY);
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

		private void CreateFieldView()
		{
			Grid = new DotView[ViewModel.Width, ViewModel.Height];
			
			for (var x = 0; x < ViewModel.Width; x++)
			{
				for (var y = 0; y < ViewModel.Height; y++)
				{
					var dotView = InstantiateDot(x, y);
					Grid[x, y] = dotView;
				}
			}
		}

		private void OnDotKilled(Position position)
		{
			var dot = Grid[position.X, position.Y];
			dot.Kill();
			RemoveChildView(dot);
			Destroy(dot.gameObject);
			Grid[position.X, position.Y] = null;
		}
		
		private void OnGridUpdated()
		{
			var gridCache = new DotView[ViewModel.Width, ViewModel.Height];
			for (var x = 0; x < ViewModel.Width; x++)
			{
				for (var y = 0; y < ViewModel.Height; y++)
				{
					var dotViewModel = ViewModel.Grid[x, y];
					if (dotViewModel == null)
					{
						continue;
					}

					var dotView = GetDotViewByViewModel(dotViewModel);
					if (dotView == null)
					{
						dotView = InstantiateDot(x, y);
						UniTask.Action(() => AddChildView(dotView)).Invoke();
					}
					else
					{
						var preferredPosition = _gridPositions[x, y];
						dotView.SetPreferredPosition(preferredPosition);
					}
					
					gridCache[x, y] = dotView;
				}
			}

			Grid = gridCache;
		}

		private DotView InstantiateDot(int x, int y)
		{
			//TODO: Use GameObject Pool
			var instantiatedView = Instantiate(_dotPrefab, _dotsPlaceholder);
			var preferredPosition = _gridPositions[x, y];
			var instantiatedPosition = new Vector2(preferredPosition.x, preferredPosition.y + _dotSpawnIndent);
			instantiatedView.SetCurrentPosition(instantiatedPosition);
			var dotViewModel = ViewModel.Grid[x, y];
			instantiatedView.SetViewModel(dotViewModel);
			instantiatedView.SetPreferredPosition(preferredPosition);

			return instantiatedView;
		}

		private DotView GetDotViewByViewModel(DotViewModel dotViewModel)
		{
			for (var x = 0; x < ViewModel.Width; x++)
			{
				for (var y = 0; y < ViewModel.Height; y++)
				{
					var dotView = Grid[x, y];
					if (dotView != null && dotView.ViewModel == dotViewModel)
					{
						return dotView;
					}
				}
			}

			return null;
		}
	}
}