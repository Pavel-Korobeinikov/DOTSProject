using DotsCore.Inputs;

namespace DotsCore.InputProcessors
{
	public interface IInputProcessor<in TInput> where TInput : Input 
	{
		void Process(TInput input, DotsField field);
	}
}