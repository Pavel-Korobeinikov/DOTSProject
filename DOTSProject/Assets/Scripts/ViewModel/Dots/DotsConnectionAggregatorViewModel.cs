using System;
using System.Collections.Generic;
using DotsCore;

namespace ViewModel.Dots
{
	public class DotsConnectionAggregatorViewModel : BaseViewModel
	{
		public event Action ConnectionUpdated;
		
		public List<DotViewModel> ConnectedDots { get; } = new List<DotViewModel>();
		
		private readonly DotsFieldViewModel _fieldViewModel;

		public DotsConnectionAggregatorViewModel(DotsFieldViewModel fieldViewModel)
		{
			_fieldViewModel = fieldViewModel;
		}

		public void AddDotConnection(Position position)
		{
			var connectedDot = _fieldViewModel.Grid[position.X, position.Y];
			ConnectedDots.Add(connectedDot);
			ConnectionUpdated?.Invoke();
		}

		public void RemoveDotConnection(Position position)
		{
			var disconnectedDot = _fieldViewModel.Grid[position.X, position.Y];
			ConnectedDots.Remove(disconnectedDot);
			ConnectionUpdated?.Invoke();
		}

		public void ClearConnections()
		{
			ConnectedDots.Clear();
			ConnectionUpdated?.Invoke();
		}
	}
}