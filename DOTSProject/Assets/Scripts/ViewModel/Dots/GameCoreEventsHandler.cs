using System;
using DotsCore.Events;
using ViewModel.Dots.CoreEventHandlers;

namespace ViewModel.Dots
{
	public static class GameCoreEventsHandlerFactory
	{
		public static ICoreEventHandler GetEventHandler(
			ICoreEvent coreEvent,
			DotsFieldViewModel fieldViewModel,
			DotsConnectionAggregatorViewModel connectionAggregatorViewModel)
		{
			switch (coreEvent)
			{
				case GridGeneratedEvent gridGeneratedEvent:
					return new GridGeneratedEventHandler(gridGeneratedEvent, fieldViewModel);
				case DotConnectedEvent dotConnectedEvent:
					return new DotConnectedEventHandler(dotConnectedEvent, connectionAggregatorViewModel);
				case DotDisconnectedEvent dotDisconnectedEvent:
					return new DotDisconnectedEventHandler(dotDisconnectedEvent, connectionAggregatorViewModel);
				case DotsConnectionClearedEvent gridGeneratedEvent:
					return new DotsConnectionClearedEventHandler(connectionAggregatorViewModel);
				case DotRemovedFromGridEvent dotRemovedFromGridEvent:
					return new DotRemovedFromGridEventHandler(dotRemovedFromGridEvent, fieldViewModel);
				case GridFallenEvent gridFallenEvent:
					return new GridFallenEventHandler(gridFallenEvent, fieldViewModel);
				case GridFilledEvent gridFilledEvent:
					return new GridFilledEventHandler(gridFilledEvent, fieldViewModel);
				default:
					throw new Exception("Event handler is missing.");
			}
		}
	}
}