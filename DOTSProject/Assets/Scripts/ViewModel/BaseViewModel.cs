using Model;
using Services;

namespace ViewModel
{
	public abstract class BaseViewModel
	{
		protected GameModel GameModel;
		protected IServiceManager ServiceManager;
	
		public void Initialize(GameModel gameModel, IServiceManager serviceManager)
		{
			GameModel = gameModel;
			ServiceManager = serviceManager;
		}

		protected TViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel, new()
		{
			var viewModel = new TViewModel();
			viewModel.Initialize(GameModel, ServiceManager);

			return viewModel;
		}
	}
}