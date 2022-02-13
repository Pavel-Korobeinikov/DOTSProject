using DotsCore;
using DotsCore.Inputs;

namespace ViewModel.Dots
{
	public class CoreInputDispatcher
	{
		private readonly DotsGame _dotsGame;

		public CoreInputDispatcher(DotsGame dotsGame)
		{
			_dotsGame = dotsGame;
		}

		public void Dispatch(IInput input)
		{
			_dotsGame.ApplyInput(input);
		}
	}
}