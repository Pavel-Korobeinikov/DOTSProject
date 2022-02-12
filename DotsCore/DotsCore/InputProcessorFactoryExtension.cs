using System;
using DotsCore.InputProcessors;
using DotsCore.Inputs;

namespace DotsCore
{
	public class InputProcessManager
	{
		private readonly DotsField _field;

		public InputProcessManager(DotsField field)
		{
			_field = field;
		}

		public void ProcessInput(Input input)
		{
			switch (input)
			{
				case AddDotsConnectionInput addDotsConnectionInput:
					var processor = new AddDotsConnectionInputProcessor();
					processor.Process(addDotsConnectionInput, _field);
					break;
			}

			throw new Exception("Unknown input");
		}
	}
}