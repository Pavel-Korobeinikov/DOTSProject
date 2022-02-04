using System.Threading.Tasks;
using Configuration.Structure;

namespace Configuration.Providers
{
	public class ScriptableObjectConfigurationProvider : IConfigurationProvider
	{
		public async Task<GameConfiguration> LoadConfiguration()
		{
			//TODO: Load configuration

			await Task.CompletedTask;
			
			return new GameConfiguration();
		}
	}
}