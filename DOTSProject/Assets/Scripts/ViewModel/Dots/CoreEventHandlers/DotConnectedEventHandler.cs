using DotsCore.Events;

namespace ViewModel.Dots.CoreEventHandlers
{
	public class DotConnectedEventHandler : ICoreEventHandler
	{
		private readonly DotConnectedEvent _coreEvent;
		private readonly DotsFieldViewModel _dotsFieldViewModel;

		public DotConnectedEventHandler(DotConnectedEvent coreEvent, DotsFieldViewModel dotsFieldViewModel)
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