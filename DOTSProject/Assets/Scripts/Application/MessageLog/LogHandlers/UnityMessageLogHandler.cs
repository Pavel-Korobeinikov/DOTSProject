using UnityEngine;

namespace Application.MessageLog.LogHandlers
{
	public class UnityMessageLogHandler : IMessageLogHandler
	{
		public void Log(string message)
		{
			Debug.Log(message);
		}

		public void LogError(string message)
		{
			Debug.LogError(message);
		}
	}
}