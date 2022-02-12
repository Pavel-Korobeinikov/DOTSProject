using System;
using System.Collections.Generic;
using Model;
using Services;

namespace ViewModel
{
	public class GameViewModel : IGameViewModel
	{
		private readonly GameModel _gameModel;
		private readonly IServiceManager _serviceManager;

		private readonly Dictionary<Type, BaseViewModel> _activeSingleViewModels = new Dictionary<Type, BaseViewModel>();
		
		public GameViewModel(GameModel gameModel, IServiceManager serviceManager)
		{
			_gameModel = gameModel;
			_serviceManager = serviceManager;
		}

		public TViewModel GetSingleViewModel<TViewModel>() where TViewModel : BaseViewModel, new()
		{
			if (_activeSingleViewModels.TryGetValue(typeof(TViewModel), out var viewModel))
			{
				viewModel.Initialize(_gameModel, _serviceManager);
				
				return (TViewModel) viewModel;
			}
			
			viewModel = new TViewModel();
			viewModel.Initialize(_gameModel, _serviceManager);
			_activeSingleViewModels.Add(typeof(TViewModel), viewModel);

			return (TViewModel) viewModel;
		}
	}
}