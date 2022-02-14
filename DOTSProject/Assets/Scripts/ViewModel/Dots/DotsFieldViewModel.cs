using System;
using DotsCore;
using DotsCore.Inputs;

namespace ViewModel.Dots
{
	public class DotsFieldViewModel : BaseViewModel
	{
		public event Action<Position> DotKilled;
		public event Action GridUpdated;

		public int Width { get; private set; }
		public int Height { get; private set; }
		public DotViewModel[,] Grid { get; private set; }

		private readonly CoreInputDispatcher _inputDispatcher;

		public DotsFieldViewModel(CoreInputDispatcher inputDispatcher)
		{
			_inputDispatcher = inputDispatcher;
		}

		public void CreateField(Dot[,] grid)
		{
			Width = grid.GetLength(0);
			Height = grid.GetLength(1);
			Grid = new DotViewModel[Width, Height];
			
			for (var x = 0; x < Width; x++)
			{
				for (var y = 0; y < Height; y++)
				{
					CreateDotViewModel(grid[x, y]);
				}
			}
		}
		
		public void RemoveDotFromField(Position position)
		{
			//TODO: Cache used of ViewModels
			var removedDot = Grid[position.X, position.Y];
			
			DotKilled?.Invoke(position);
			
			removedDot.Destroy();
			Grid[position.X, position.Y] = null;
		}

		public void UpdateGrid(Dot[,] grid)
		{
			var gridCache = new DotViewModel[Width, Height];
			for (var x = 0; x < Width; x++)
			{
				for (var y = 0; y < Height; y++)
				{
					var dot = grid[x, y];
					var dotViewModel = GetDotViewModel(dot);

					if (dot != null && dotViewModel != null)
					{
						UpdateDotViewModel(dotViewModel, dot);
					}
					else if (dot != null)
					{
						dotViewModel = CreateDotViewModel(dot);
					}
					
					gridCache[x, y] = dotViewModel;
				}
			}

			Grid = gridCache;
			
			GridUpdated?.Invoke();
		}

		public void DotsPressFinished()
		{
			_inputDispatcher.Dispatch(new ApplySelectionInput());
		}

		private DotViewModel CreateDotViewModel(Dot dot)
		{
			var x = dot.Position.X;
			var y = dot.Position.Y;
			var dotViewModel = CreateViewModel(() => new DotViewModel(dot));
			dotViewModel.SetDotPosition(dot.Position);
			dotViewModel.Pressed += OnDotPressed;
			dotViewModel.PressFinished += OnDotPressFinished;
			Grid[x, y] = dotViewModel;

			return dotViewModel;
		}

		private void OnDotPressed(DotViewModel dotViewModel)
		{
			_inputDispatcher.Dispatch(new DotSelectedInput(dotViewModel.Position));
		}

		private void OnDotPressFinished(DotViewModel dotViewModel)
		{
			DotsPressFinished();
		}

		private void UpdateDotViewModel(DotViewModel dotViewModel, Dot dot)
		{
			dotViewModel.SetDotPosition(dot.Position);
		}

		private DotViewModel GetDotViewModel(Dot dot)
		{
			for (var x = 0; x < Width; x++)
			{
				for (var y = 0; y < Height; y++)
				{
					if (Grid[x, y]?.DotData == dot)
					{
						return Grid[x, y];
					}
				}
			}

			return null;
		}
	}
}