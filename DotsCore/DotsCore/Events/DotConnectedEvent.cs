namespace DotsCore.Events
{
	public class DotConnectedEvent : ICoreEvent
	{
		public int X { get; }
		public int Y { get; }

		public DotConnectedEvent(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}