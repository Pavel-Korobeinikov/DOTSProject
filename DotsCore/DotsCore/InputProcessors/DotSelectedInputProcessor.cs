using DotsCore.Inputs;

namespace DotsCore.InputProcessors
{
	public class DotSelectedInputProcessor : IInputProcessor<DotSelectedInput>
	{
		public void Process(
			DotSelectedInput input,
			DotsConnectionAggregator dotsConnectionAggregator)
		{
			var connections = dotsConnectionAggregator.Connections;
			if (connections.Count > 1 && connections[connections.Count - 2].Position.Equals(input.Position))
			{
				dotsConnectionAggregator.TryRemoveConnection(input.Position);
			}
			else
			{
				dotsConnectionAggregator.TryAddConnection(input.Position);
			}
		}
	}
}