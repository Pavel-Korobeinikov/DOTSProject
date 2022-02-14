using System.Collections.Generic;
using System.Linq;
using DotsCore.DotsCombinations;

namespace DotsCore
{
	public class DotsConnectionCombiner
	{
		private readonly DotsField _field;
		private readonly EventsNotifier _eventsNotifier;

		private readonly List<IDotsCombiner> _combiners = new List<IDotsCombiner>();

		public DotsConnectionCombiner(DotsField field, EventsNotifier eventsNotifier)
		{
			_field = field;
			_eventsNotifier = eventsNotifier;
			
			_combiners.Add(new SquareDotCombiner());
		}

		public bool TryCombineDots(List<Dot> connections)
		{
			var dotsCombiner = _combiners.FirstOrDefault(combiner => combiner.CanCombine(connections));
			if (dotsCombiner == null)
			{
				return false;
			}
			
			dotsCombiner.Combine(connections, _field, _eventsNotifier);
			return true;
		}
	}
}