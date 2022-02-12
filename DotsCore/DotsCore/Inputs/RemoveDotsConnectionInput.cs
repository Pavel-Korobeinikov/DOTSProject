namespace DotsCore.Inputs
{
	public class RemoveDotsConnectionInput : IInput
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