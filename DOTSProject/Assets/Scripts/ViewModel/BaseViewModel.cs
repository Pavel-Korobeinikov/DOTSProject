using System;
using Model;
using Services;

namespace ViewModel
{
	public abstract class BaseViewModel
	{
		protected GameModel GameModel;
		protected IServiceManager ServiceManager;
	
		public virtual void Initialize(GameModel gameModel, IServiceManager serviceManager)
		{
			GameModel = gameModel;
			ServiceManager = serviceManager;
		}

		protected TViewModel CreateViewModel<TViewModel>(Func<TViewModel> createFunc) where TViewModel : BaseViewModel
		{
			var viewModel = createFunc.Invoke();
			viewModel.Initialize(GameModel, ServiceManager);

			return viewModel;
		}
	}
}