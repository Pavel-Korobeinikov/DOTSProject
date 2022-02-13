using System;
using DotsCore.Events;
using DotsCore.Inputs;

namespace DotsCore
{
	public class DotsGame
	{
		private readonly DotsField _field;
		private readonly InputProcessManager _inputProcessManager;
		private readonly EventsNotifier _eventsNotifier;

		public DotsGame(DotsGameInitializationData initializationData)
		{
			var random = new Random(initializationData.Seed);
			_eventsNotifier = new EventsNotifier();
			_field = new DotsField(
				initializationData.Width,
				initializationData.Height,
				initializationData.Colors,
				random,
				_eventsNotifier);
			var dotsConnectionAggregator = new DotsConnectionAggregator(_field, _eventsNotifier);
			_inputProcessManager = new InputProcessManager(dotsConnectionAggregator);
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