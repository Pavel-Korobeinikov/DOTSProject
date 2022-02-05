using Cysharp.Threading.Tasks;
using Services.Configuration.Structure;

namespace Services.Configuration.Providers
{
	public interface IConfigurationProvider
	{
		public UniTask<GameConfigurationEntity> LoadConfiguration();
	}
}