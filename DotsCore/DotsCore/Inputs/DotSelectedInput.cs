namespace DotsCore.Inputs
{
	public class DotSelectedInput : IInput
	{
		public int X { get; }
		public int Y { get; }

		public DotSelectedInput(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}