namespace Application.MessageLog.LogHandlers
{
	public interface IMessageLogHandler
	{
		public void LogError(string message);
	}
}