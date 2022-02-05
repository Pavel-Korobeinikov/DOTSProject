using Services.Configuration.Structure;

namespace Services.Configuration
{
	public interface IConfigurationService : IService
	{
		GameConfigurationEntity GameConfiguration { get; }
	}
}