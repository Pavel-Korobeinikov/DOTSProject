using DotsCore.Inputs;

namespace DotsCore.InputProcessors
{
	public class ApplyConnectionsInputProcessor : IInputProcessor<ApplyConnectionsInput>
	{
		public void Process(ApplyConnectionsInput input, DotsField field)
		{
			field.RemoveConnectionDotsFromGrid();
			field.Fall();
			field.Fill();
		}
	}
}