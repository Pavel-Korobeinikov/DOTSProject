using Configuration.Structure.Scenes;
using Cysharp.Threading.Tasks;

namespace ViewModel.SceneManagement
{
	public abstract class SceneBase
	{
		public SceneEntity Entity { get; set; }

		public virtual void Initialize() { }

		public virtual UniTask Activate()
		{
			return UniTask.CompletedTask;
		}

		public virtual UniTask Deactivate()
		{
			return UniTask.CompletedTask;
		}

		public void Utilize() { }
	}
}