namespace DotsCore.Events
{
	public class DotDisconnectedEvent : ICoreEvent
	{
		public Position Position { get; }

		public DotDisconnectedEvent(Position position)
		{
			Position = position;
		}
	}
}