namespace DotsCore.Inputs
{
	public class DotSelectedInput : IInput
	{
		public Position Position { get; }

		public DotSelectedInput(Position position)
		{
			Position = position;
		}
	}
}