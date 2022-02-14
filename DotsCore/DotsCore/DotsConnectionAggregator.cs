using System;
using System.Collections.Generic;
using System.Linq;
using DotsCore.Events;

namespace DotsCore
{
	public class DotsConnectionAggregator
	{
		private readonly DotsField _field;
		private readonly EventsNotifier _eventsNotifier;
		private readonly List<Dot> _connections = new List<Dot>();
		private readonly DotsConnectionCombiner _dotsConnectionCombiner;

		public List<Dot> Connections => _connections;

		public DotsConnectionAggregator(DotsField field, EventsNotifier eventsNotifier)
		{
			_field = field;
			_eventsNotifier = eventsNotifier;

			_dotsConnectionCombiner = new DotsConnectionCombiner(_field, _eventsNotifier);
		}

		public void TryAddConnection(Position toPosition)
		{
			var fromDotConnection = _connections.LastOrDefault();
			var toDotConnection = _field.Grid[toPosition.X, toPosition.Y];

			if (fromDotConnection == null ||
			    !fromDotConnection.Position.Equals(toPosition) &&
			    fromDotConnection.Color.Name == toDotConnection.Color.Name &&
			    Math.Abs(fromDotConnection.Position.X - toPosition.X) <= 1 && 
			    Math.Abs(fromDotConnection.Position.Y - toPosition.Y) <= 1 &&
			    (Math.Abs(fromDotConnection.Position.X - toPosition.X) != 1 || Math.Abs(fromDotConnection.Position.Y - toPosition.Y) != 1))
			{
				_connections.Add(toDotConnection);
				
				_eventsNotifier.RiseEvent(new DotConnectedEvent(toPosition));
			}
		}

		public void TryRemoveConnection(Position fromPosition)
		{
			var previousDotConnection = _field.Grid[fromPosition.X, fromPosition.Y];
			
			if (_connections.Count > 1 &&
			    _connections[_connections.Count - 2] == previousDotConnection)
			{
				var currentDotConnection = _connections.Last();
				_connections.Remove(currentDotConnection);
				
				_eventsNotifier.RiseEvent(new DotDisconnectedEvent(currentDotConnection.Position));
			}
		}
		
		public void ApplyConnections()
		{
			if (_connections.Count > 1 &&
			    !_dotsConnectionCombiner.TryCombineDots(_connections))
			{
				foreach (var dot in _connections)
				{
					_field.RemoveDotFromGrid(dot.Position);
				}
				
				_field.Fall();
				_field.Fill();
			}
			
			_connections.Clear();
			_eventsNotifier.RiseEvent(new DotsConnectionClearedEvent());
		}
	}
}