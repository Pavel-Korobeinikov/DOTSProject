using System;
using DotsCore.Events;
using DotsCore.Inputs;

namespace DotsCore
{
	public class DotsGame
	{
		private readonly Random _random;
		private readonly DotsField _field;
		private readonly InputProcessManager _inputProcessManager;
		private readonly EventsNotifier _eventsNotifier;

		public DotsGame(DotsGameInitializationData initializationData)
		{
			_random = new Random(initializationData.Seed);
			_eventsNotifier = new EventsNotifier();
			_field = new DotsField(
				initializationData.Width,
				initializationData.Height,
				initializationData.Colors,
				_random,
				_eventsNotifier);
			_inputProcessManager = new InputProcessManager(_field);
		}

		public void Launch()
		{
			_field.Generate();
		}

		public void ApplyInput(IInput input)
		{
			_inputProcessManager.ProcessInput(input);
		}

		public void SubscribeOutputPort(Action<ICoreEvent> outputPort)
		{
			_eventsNotifier.SubscribeOutputPort(outputPort);
		}
	}
}