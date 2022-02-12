using System;
using DotsCore.Events;
using ViewModel.Dots.CoreEventHandlers;

namespace ViewModel.Dots
{
	public static class GameCoreEventsHandlerFactory
	{
		public static ICoreEventHandler GetEventHandler(ICoreEvent coreEvent, DotsFieldViewModel fieldViewModel)
		{
			switch (coreEvent)
			{
				case GridGeneratedEvent gridGeneratedEvent:
					return new GridGeneratedEventHandler(gridGeneratedEvent, fieldViewModel);
			}

			throw new Exception("Event handler is missing.");
		}
	}
}