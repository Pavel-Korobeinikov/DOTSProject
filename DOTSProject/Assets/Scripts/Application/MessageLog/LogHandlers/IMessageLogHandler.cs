namespace Application.MessageLog.LogHandlers
{
	public interface IMessageLogHandler
	{
		public void Log(string message);
		public void LogError(string message);
	}
}