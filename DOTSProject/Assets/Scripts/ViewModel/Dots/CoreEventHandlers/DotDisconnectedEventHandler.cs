using DotsCore.Events;

namespace ViewModel.Dots.CoreEventHandlers
{
	public class DotDisconnectedEventHandler : ICoreEventHandler
	{
		private readonly DotDisconnectedEvent _coreEvent;
		private readonly DotsConnectionAggregatorViewModel _connectionAggregatorViewModel;

		public DotDisconnectedEventHandler(DotDisconnectedEvent coreEvent, DotsConnectionAggregatorViewModel connectionAggregatorViewModel)
		{
			_coreEvent = coreEvent;
			_connectionAggregatorViewModel = connectionAggregatorViewModel;
		}

		public void Handle()
		{
			_connectionAggregatorViewModel.RemoveDotConnection(_coreEvent.Position);
		}
	}
}