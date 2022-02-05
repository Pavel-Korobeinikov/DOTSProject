using Configuration.Structure.Scenes;
using Cysharp.Threading.Tasks;

namespace ViewModel.SceneManagement.Scenes
{
	public interface ISceneViewModel
	{
		public SceneEntity Entity { get; set; }
		public void Initialize();
		public UniTask Activate();
		public UniTask Deactivate();
		public void Utilize();
	}
}