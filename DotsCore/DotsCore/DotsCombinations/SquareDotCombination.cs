using System.Collections.Generic;
using System.Linq;

namespace DotsCore.DotsCombinations
{
	public class SquareDotCombiner : IDotsCombiner
	{
		private readonly List<Dot> _dotsCache = new List<Dot>();
		
		public bool CanCombine(List<Dot> connections)
		{
			return connections.Select(connectionDot => connections
				.Where(dot => dot.Position.X == connectionDot.Position.X && dot.Position.Y == connectionDot.Position.Y))
				.Any(duplicates => duplicates.Count() > 1);
		}

		public void Combine(List<Dot> connections, DotsField field, EventsNotifier eventsNotifier)
		{
			var connectionColor = connections.First().Color;
			
			_dotsCache.Clear();
			foreach (var dot in field.Grid)
			{
				if (dot.Color.Name == connectionColor.Name)
				{
					_dotsCache.Add(dot);
				}
			}

			foreach (var dot in _dotsCache)
			{
				field.RemoveDotFromGrid(dot.Position);
			}
			
			field.Fall();
			field.Fill();
		}
	}
}