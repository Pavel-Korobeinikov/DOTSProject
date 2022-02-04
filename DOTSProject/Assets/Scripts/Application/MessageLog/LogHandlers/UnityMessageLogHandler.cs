using UnityEngine;

namespace Application.MessageLog.LogHandlers
{
	public class UnityMessageLogHandler : IMessageLogHandler
	{
		public void LogError(string message)
		{
			Debug.LogError(message);
		}
	}
}