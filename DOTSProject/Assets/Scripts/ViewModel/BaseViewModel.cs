using Model;
using Services;

namespace ViewModel
{
	public abstract class BaseViewModel
	{
		protected GameModel _gameModel;
		protected IServiceManager _serviceManager;
	
		public virtual void Initialize(GameModel gameModel, IServiceManager serviceManager)
		{
			_gameModel = gameModel;
			_serviceManager = serviceManager;
		}

		protected TViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel, new()
		{
			var viewModel = new TViewModel();
			viewModel.Initialize(_gameModel, _serviceManager);

			return viewModel;
		}
	}
}