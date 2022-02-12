namespace DotsCore.Events
{
	public class DotRemovedFromGridEvent : ICoreEvent
	{
		public int X { get; }
		public int Y { get; }

		public DotRemovedFromGridEvent(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}