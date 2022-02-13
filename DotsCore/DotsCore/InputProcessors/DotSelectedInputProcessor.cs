using System.Linq;
using DotsCore.Inputs;

namespace DotsCore.InputProcessors
{
	public class DotSelectedInputProcessor : IInputProcessor<DotSelectedInput>
	{
		public void Process(
			DotSelectedInput input,
			DotsConnectionAggregator dotsConnectionAggregator)
		{
			var lastSelectedDot = dotsConnectionAggregator.Connections.LastOrDefault();
			var selectedDotPosition = new Position(input.X, input.Y);
			if (lastSelectedDot == null || !lastSelectedDot.Position.Equals(selectedDotPosition))
			{
				dotsConnectionAggregator.TryAddConnection(selectedDotPosition);
			}
			else
			{
				dotsConnectionAggregator.TryRemoveConnection(selectedDotPosition);
			}
		}
	}
}