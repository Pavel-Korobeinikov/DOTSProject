using Cysharp.Threading.Tasks;

namespace ViewModel
{
	public interface IViewModel
	{
		public void Initialize();
		public UniTask Activate();
		public UniTask Deactivate();
		public void Utilize();
	}
}