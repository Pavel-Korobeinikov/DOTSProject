using System.Threading.Tasks;
using Configuration.Structure;

namespace Configuration.Providers
{
	public interface IConfigurationProvider
	{
		public Task<GameConfiguration> LoadConfiguration();
	}
}