namespace DotsCore.Events
{
	public class DotConnectedEvent : ICoreEvent
	{
		public Position Position { get; }

		public DotConnectedEvent(Position position)
		{
			Position = position;
		}
	}
}