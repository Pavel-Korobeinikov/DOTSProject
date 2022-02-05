using Cysharp.Threading.Tasks;

namespace Services
{
	public interface IInitializable
	{
		UniTask Initialize();
	}
}