using DotsCore.Events;

namespace ViewModel.Dots.CoreEventHandlers
{
	public class DotConnectedEventHandler : ICoreEventHandler
	{
		private readonly DotConnectedEvent _coreEvent;
		private readonly DotsConnectionAggregatorViewModel _connectionAggregatorViewModel;

		public DotConnectedEventHandler(DotConnectedEvent coreEvent, DotsConnectionAggregatorViewModel connectionAggregatorViewModel)
		{
			_coreEvent = coreEvent;
			_connectionAggregatorViewModel = connectionAggregatorViewModel;
		}
		
		public void Handle()
		{
			_connectionAggregatorViewModel.AddDotConnection(_coreEvent.Position);
		}
	}
}