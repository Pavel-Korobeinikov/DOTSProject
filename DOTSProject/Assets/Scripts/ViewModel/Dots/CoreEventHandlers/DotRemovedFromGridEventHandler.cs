using Application.MessageLog;
using DotsCore.Events;

namespace ViewModel.Dots.CoreEventHandlers
{
	public class DotRemovedFromGridEventHandler : ICoreEventHandler
	{
		private readonly DotRemovedFromGridEvent _coreEvent;
		private readonly DotsFieldViewModel _dotsFieldViewModel;

		public DotRemovedFromGridEventHandler(DotRemovedFromGridEvent coreEvent, DotsFieldViewModel dotsFieldViewModel)
		{
			_coreEvent = coreEvent;
			_dotsFieldViewModel = dotsFieldViewModel;
		}
		
		public void Handle()
		{
			_dotsFieldViewModel.RemoveDotFromField(_coreEvent.Position);
		}
	}
}