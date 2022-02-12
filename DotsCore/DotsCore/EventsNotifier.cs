using System;
using System.Collections.Generic;
using DotsCore.Events;

namespace DotsCore
{
	public class EventsNotifier
	{
		private List<Action<ICoreEvent>> _subscribers = new List<Action<ICoreEvent>>();
		
		public void SubscribeOutputPort(Action<ICoreEvent> outputPort)
		{
			_subscribers.Add(outputPort);
		}

		public void RiseEvent(ICoreEvent coreEvent)
		{
			foreach (var subscriber in _subscribers)
			{
				subscriber.Invoke(coreEvent);
			}
		}
	}
}