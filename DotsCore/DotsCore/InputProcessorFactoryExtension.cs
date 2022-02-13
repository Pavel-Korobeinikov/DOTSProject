using System;
using DotsCore.InputProcessors;
using DotsCore.Inputs;

namespace DotsCore
{
	public class InputProcessManager
	{
		private readonly DotsConnectionAggregator _dotsConnectionAggregator;

		public InputProcessManager(DotsConnectionAggregator dotsConnectionAggregator)
		{
			_dotsConnectionAggregator = dotsConnectionAggregator;
		}

		public void ProcessInput(IInput input)
		{
			switch (input)
			{
				case DotSelectedInput dotSelectedInput:
					var dotSelectedInputProcessor = new DotSelectedInputProcessor();
					dotSelectedInputProcessor.Process(dotSelectedInput, _dotsConnectionAggregator);
					break;
				case ApplySelectionInput applySelectionInput:
					var applySelectionInputProcessor = new ApplySelectionInputProcessor();
					applySelectionInputProcessor.Process(applySelectionInput, _dotsConnectionAggregator);
					break;
				default:
					throw new Exception("Unknown input");
			}
		}
	}
}