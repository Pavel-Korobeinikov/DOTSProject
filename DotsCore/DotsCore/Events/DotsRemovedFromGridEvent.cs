namespace DotsCore.Events
{
	public class DotRemovedFromGridEvent : ICoreEvent
	{
		public Position Position { get; }

		public DotRemovedFromGridEvent(Position position)
		{
			Position = position;
		}
	}
}