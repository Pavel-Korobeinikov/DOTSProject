using DotsCore.Inputs;

namespace DotsCore.InputProcessors
{
	public class RemoveDotsConnectionInputProcessor : IInputProcessor<RemoveDotsConnectionInput>
	{
		public void Process(RemoveDotsConnectionInput input, DotsField field)
		{
			field.RemoveConnection(
				new Position(input.FromX, input.FromY));
		}
	}
}