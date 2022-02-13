using DotsCore.Inputs;

namespace DotsCore.InputProcessors
{
	public interface IInputProcessor<in TInput> where TInput : IInput 
	{
		void Process(
			TInput input,
			DotsConnectionAggregator dotsConnectionAggregator);
	}
}