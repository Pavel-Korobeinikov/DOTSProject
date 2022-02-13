using System;
using DotsCore;
using DotsCore.Inputs;

namespace ViewModel.Dots
{
	public class DotsFieldViewModel : BaseViewModel
	{
		public event Action<int, int> DotKilled;

		public int Width { get; private set; }
		public int Height { get; private set; }
		public DotViewModel[,] Grid { get; private set; }

		private CoreInputDispatcher _inputDispatcher;

		public void SetCoreInputDispatcher(CoreInputDispatcher inputDispatcher)
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
					var dotViewModel = CreateViewModel<DotViewModel>();
					dotViewModel.SetDotInfo(grid[x, y]);
					dotViewModel.SubscribeOnPressEvent(OnDotPressed);
					dotViewModel.SubscribeOnPressEndedEvent(OnDotPressEnded);
					Grid[x, y] = dotViewModel;
				}
			}
		}
		
		public void RemoveDotFromField(int x, int y)
		{
			//TODO: Cache used of ViewModels
			var removedDot = Grid[x, y];
			
			DotKilled?.Invoke(x, y);
			
			removedDot.Destroy();
			Grid[x, y] = null;
		}

		private void OnDotPressed(DotViewModel dotViewModel)
		{
			_inputDispatcher.Dispatch(new DotSelectedInput(dotViewModel.X, dotViewModel.Y));
		}

		private void OnDotPressEnded(DotViewModel obj)
		{
			DotsPressFinished();
		}

		public void DotsPressFinished()
		{
			_inputDispatcher.Dispatch(new ApplySelectionInput());
		}
	}
}