namespace ViewModel
{
	public interface IGameViewModel
	{
		TViewModel GetSingleViewModel<TViewModel>() where TViewModel : BaseViewModel, new();
	}
}