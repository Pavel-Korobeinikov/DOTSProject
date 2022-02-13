namespace DotsCore.Events
{
	public class DotDisconnectedEvent : ICoreEvent
	{
		public int X { get; }
		public int Y { get; }

		public DotDisconnectedEvent(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}