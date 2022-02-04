using Configuration.Structure;
using Cysharp.Threading.Tasks;

namespace Configuration.Providers
{
	public class ScriptableObjectConfigurationProvider : IConfigurationProvider
	{
		public async UniTask<GameConfiguration> LoadConfiguration()
		{
			//TODO: Load configuration

			await UniTask.CompletedTask;
			
			return new GameConfiguration();
		}
	}
}