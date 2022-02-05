using Configuration.Structure;
using Cysharp.Threading.Tasks;

namespace Services.Configuration.Providers
{
	public interface IConfigurationProvider
	{
		public UniTask<GameConfigurationEntity> LoadConfiguration();
	}
}