namespace DotsCore.Inputs
{
	public class AddDotsConnectionInput : Input
	{
		public int FromX { get; }
		public int FromY { get; }
		public int ToX { get; }
		public int ToY { get; }

		public AddDotsConnectionInput(
			int fromX, int fromY,
			int toX, int toY)
		{
			FromX = fromX;
			FromY = fromY;
			ToX = toX;
			ToY = toY;
		}
	}
}