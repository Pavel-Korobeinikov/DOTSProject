using DotsCore.Inputs;

namespace DotsCore.InputProcessors
{
	public class AddDotsConnectionInputProcessor : IInputProcessor<AddDotsConnectionInput>
	{
		public void Process(AddDotsConnectionInput input, DotsField field)
		{
			field.AddConnection(
				new Position(input.FromX, input.FromY),
				new Position(input.ToX, input.ToY));
		}
	}
}