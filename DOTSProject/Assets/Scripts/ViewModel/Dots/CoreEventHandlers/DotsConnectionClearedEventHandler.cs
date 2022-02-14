using DotsCore.Events;

namespace ViewModel.Dots.CoreEventHandlers
{
	public class DotsConnectionClearedEventHandler : ICoreEventHandler
	{
		private readonly DotsConnectionAggregatorViewModel _connectionAggregatorViewModel;

		public DotsConnectionClearedEventHandler(DotsConnectionAggregatorViewModel connectionAggregatorViewModel)
		{
			_connectionAggregatorViewModel = connectionAggregatorViewModel;
		}

		public void Handle()
		{
			_connectionAggregatorViewModel.ClearConnections();
		}
	}
}