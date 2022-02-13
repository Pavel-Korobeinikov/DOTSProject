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
				case DotConnectedEvent gridGeneratedEvent:
					return new DotConnectedEventHandler(gridGeneratedEvent, fieldViewModel);
				case DotDisconnectedEvent gridGeneratedEvent:
					return new DotDisconnectedEventHandler(gridGeneratedEvent, fieldViewModel);
				case DotRemovedFromGridEvent gridGeneratedEvent:
					return new DotRemovedFromGridEventHandler(gridGeneratedEvent, fieldViewModel);
				case GridFallenEvent gridGeneratedEvent:
					return new GridFallenEventHandler(gridGeneratedEvent, fieldViewModel);
				case GridFilledEvent gridGeneratedEvent:
					return new GridFilledEventHandler(gridGeneratedEvent, fieldViewModel);
				case GridGeneratedEvent gridGeneratedEvent:
					return new GridGeneratedEventHandler(gridGeneratedEvent, fieldViewModel);
				default:
					throw new Exception("Event handler is missing.");
			}
		}
	}
}