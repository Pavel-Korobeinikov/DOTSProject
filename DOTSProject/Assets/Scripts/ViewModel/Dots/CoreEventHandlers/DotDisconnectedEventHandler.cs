using DotsCore.Events;

namespace ViewModel.Dots.CoreEventHandlers
{
	public class DotDisconnectedEventHandler : ICoreEventHandler
	{
		private readonly DotDisconnectedEvent _coreEvent;
		private readonly DotsFieldViewModel _dotsFieldViewModel;

		public DotDisconnectedEventHandler(DotDisconnectedEvent coreEvent, DotsFieldViewModel dotsFieldViewModel)
		{
			_coreEvent = coreEvent;
			_dotsFieldViewModel = dotsFieldViewModel;
		}

		public void Handle()
		{
			//TODO: Handle
		}
	}
}