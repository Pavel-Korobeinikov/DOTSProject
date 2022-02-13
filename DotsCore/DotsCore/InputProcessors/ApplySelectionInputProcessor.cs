using DotsCore.Inputs;

namespace DotsCore.InputProcessors
{
	public class ApplySelectionInputProcessor : IInputProcessor<ApplySelectionInput>
	{
		public void Process(
			ApplySelectionInput input,
			DotsConnectionAggregator dotsConnectionAggregator)
		{
			dotsConnectionAggregator.ApplyConnections();
		}
	}
}