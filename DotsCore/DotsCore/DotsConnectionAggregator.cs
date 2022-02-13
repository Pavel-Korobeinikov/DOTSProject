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

		public List<Dot> Connections => _connections;

		public DotsConnectionAggregator(DotsField field, EventsNotifier eventsNotifier)
		{
			_field = field;
			_eventsNotifier = eventsNotifier;
		}

		public void TryAddConnection(Position toPosition)
		{
			var fromDotConnection = _connections.LastOrDefault();
			var toDotConnection = _field.Grid[toPosition.X, toPosition.Y];

			if (fromDotConnection == null ||
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
			var lastDotConnection = _field.Grid[fromPosition.X, fromPosition.Y];
			
			if (_connections.Count > 1 &&
			    _connections[_connections.Count - 2] == lastDotConnection)
			{
				_connections.Remove(lastDotConnection);
				
				_eventsNotifier.RiseEvent(new DotDisconnectedEvent(fromPosition));
			}
		}
		
		public void ApplyConnections()
		{
			if (_connections.Count > 1)
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