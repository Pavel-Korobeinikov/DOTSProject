using ViewModel;

namespace View.Scenes
{
	public class SceneView<TViewModel> : BaseView, ISceneView where TViewModel : BaseViewModel, new()
	{
		protected TViewModel ViewModel { get; set; }
		
		public void SetViewModel(IGameViewModel gameViewModel)
		{
			ViewModel = gameViewModel.GetSingleViewModel<TViewModel>();
		}
	}
}