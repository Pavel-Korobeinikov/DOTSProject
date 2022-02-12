using DotsCore;

namespace ViewModel.Dots
{
	public class DotsFieldViewModel : BaseViewModel
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public DotViewModel[,] Grid { get; private set; }

		public void GenerateField(Dot[,] grid)
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
					Grid[x, y] = dotViewModel;
				}
			}
		}
	}
}