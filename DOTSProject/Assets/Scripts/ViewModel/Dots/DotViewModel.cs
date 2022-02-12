using System.Linq;
using DotsCore;
using Services.Configuration;

namespace ViewModel.Dots
{
	public class DotViewModel : BaseViewModel
	{
		public float R { get; private set; }
		public float G { get; private set; }
		public float B { get; private set; }

		public void SetDotInfo(Dot dot)
		{
			var gameConfiguration = _serviceManager.GetService<IConfigurationService>().GameConfiguration;
			var dotEntities = gameConfiguration.BattleConfiguration.Dots;
			var dotEntity = dotEntities.First(entity => entity.Color.Name == dot.Color.Name);

			R = dotEntity.Color.R;
			G = dotEntity.Color.G;
			B = dotEntity.Color.B;
		}
	}
}