using DotsCore.Events;

namespace ViewModel.Dots.CoreEventHandlers
{
	public class GridFallenEventHandler : ICoreEventHandler
	{
		private readonly GridFallenEvent _coreEvent;
		private readonly DotsFieldViewModel _dotsFieldViewModel;

		public GridFallenEventHandler(GridFallenEvent coreEvent, DotsFieldViewModel dotsFieldViewModel)
		{
			_coreEvent = coreEvent;
			_dotsFieldViewModel = dotsFieldViewModel;
		}
		
		public void Handle()
		{
			_dotsFieldViewModel.UpdateGrid(_coreEvent.UpdatedGrid);
		}
	}
}