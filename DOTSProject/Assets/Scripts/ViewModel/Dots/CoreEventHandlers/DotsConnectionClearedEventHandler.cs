using DotsCore.Events;

namespace ViewModel.Dots.CoreEventHandlers
{
	public class DotsConnectionClearedEventHandler : ICoreEventHandler
	{
		private readonly DotsConnectionClearedEvent _coreEvent;
		private readonly DotsFieldViewModel _dotsFieldViewModel;

		public DotsConnectionClearedEventHandler(DotsConnectionClearedEvent coreEvent, DotsFieldViewModel dotsFieldViewModel)
		{
			_coreEvent = coreEvent;
			_dotsFieldViewModel = dotsFieldViewModel;
		}

		public void Handle()
		{
			
		}
	}
}