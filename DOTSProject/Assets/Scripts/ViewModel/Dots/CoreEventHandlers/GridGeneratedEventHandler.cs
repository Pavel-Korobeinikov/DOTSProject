using DotsCore.Events;

namespace ViewModel.Dots.CoreEventHandlers
{
	public class GridGeneratedEventHandler : ICoreEventHandler
	{
		private readonly GridGeneratedEvent _coreEvent;
		private readonly DotsFieldViewModel _dotsFieldViewModel;

		public GridGeneratedEventHandler(GridGeneratedEvent coreEvent, DotsFieldViewModel dotsFieldViewModel)
		{
			_coreEvent = coreEvent;
			_dotsFieldViewModel = dotsFieldViewModel;
		}

		public void Handle()
		{
			_dotsFieldViewModel.GenerateField(_coreEvent.UpdatedGrid);
		}
	}
}