using System;
using DotsCore.Inputs;

namespace DotsCore
{
	public class DotsGame
	{
		private readonly Random _random;
		private readonly DotsField _field;
		private readonly InputProcessManager _inputProcessManager;
		
		public DotsGame(DotsGameInitializationData initializationData)
		{
			_random = new Random(initializationData.Seed);
			_field = new DotsField(
				initializationData.Width,
				initializationData.Height,
				initializationData.Colors,
				_random);
			_inputProcessManager = new InputProcessManager(_field);
		}

		public void Launch()
		{
			_field.Generate();
		}

		public void ApplyInput(Input input)
		{
			_inputProcessManager.ProcessInput(input);
		}
	}
}