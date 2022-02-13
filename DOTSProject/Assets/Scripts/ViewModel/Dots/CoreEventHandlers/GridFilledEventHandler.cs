using DotsCore.Events;

namespace ViewModel.Dots.CoreEventHandlers
{
	public class GridFilledEventHandler : ICoreEventHandler
	{
		private readonly GridFilledEvent _coreEvent;
		private readonly DotsFieldViewModel _dotsFieldViewModel;

		public GridFilledEventHandler(GridFilledEvent coreEvent, DotsFieldViewModel dotsFieldViewModel)
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