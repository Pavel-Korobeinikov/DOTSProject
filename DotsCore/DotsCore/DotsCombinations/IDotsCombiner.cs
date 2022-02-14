using System.Collections.Generic;

namespace DotsCore.DotsCombinations
{
	public interface IDotsCombiner
	{
		bool CanCombine(List<Dot> connections);
		void Combine(List<Dot> connections, DotsField field, EventsNotifier eventsNotifier);
	}
}