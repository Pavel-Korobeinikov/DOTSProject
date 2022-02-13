using Cysharp.Threading.Tasks;
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

		private float _dotWidth;
		private float _dotHeight;
		private Vector2[,] _gridPositions;

		public DotView[,] Grid { get; private set; }

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
				AddChildView(dotView);
			}
		}

		protected override void Subscribe()
		{
			ViewModel.DotKilled += OnDotKilled;
		}

		protected override UniTask Activate()
		{
			return UniTask.CompletedTask;
		}

		protected override void Unsubscribe()
		{
			ViewModel.DotKilled -= OnDotKilled;
		}

		private void FillFieldPositions()
		{
			_gridPositions = new Vector2[ViewModel.Width, ViewModel.Height];
			
			for (var x = 0; x < ViewModel.Width; x++)
			{
				for (var y = 0; y < ViewModel.Height; y++)
				{
					var positionX = (_dotHorizontalSpacing * x) + (_dotWidth * x);
					var positionY = -(_dotVerticalSpacing * y) - (_dotHeight * y);
					
					_gridPositions[x, y] = new Vector2(positionX, positionY);
				}
			}
		}

		private void CreateFieldView()
		{
			Grid = new DotView[ViewModel.Width, ViewModel.Height];
			
			for (var x = 0; x < ViewModel.Width; x++)
			{
				for (var y = 0; y < ViewModel.Height; y++)
				{
					//TODO: Use GameObject Pool
					var instantiatedView = Instantiate(_dotPrefab, _dotsPlaceholder);
					var preferredPosition = _gridPositions[x, y];
					var dotViewModel = ViewModel.Grid[x, y];
					instantiatedView.SetViewModel(dotViewModel);
					instantiatedView.SetPreferredPosition(preferredPosition);
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
			var dot = Grid[x, y];
			dot.Kill();
			Destroy(dot.gameObject);
			Grid[x, y] = null;
		}
	}
}