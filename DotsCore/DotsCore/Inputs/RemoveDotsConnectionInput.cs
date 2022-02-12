namespace DotsCore.Inputs
{
	public class RemoveDotsConnectionInput : Input
	{
		public int FromX { get; }
		public int FromY { get; }

		public RemoveDotsConnectionInput(
			int fromX, int fromY)
		{
			FromX = fromX;
			FromY = fromY;
		}
	}
}