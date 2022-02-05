using Configuration.Structure;
using Cysharp.Threading.Tasks;

namespace Configuration.Providers
{
	public interface IConfigurationProvider
	{
		public UniTask<GameConfigurationEntity> LoadConfiguration();
	}
}