using Application.MessageLog.LogHandlers;

namespace Application.MessageLog
{
	public static class MessageLogger
	{
		private static IMessageLogHandler _logHandler;
		
		public static IMessageLogHandler LogHandler
		{
			get => _logHandler;
			set => _logHandler ??= value;
		}

		public static void LogError(string message)
		{
			_logHandler?.LogError(message);
		}
	}
}