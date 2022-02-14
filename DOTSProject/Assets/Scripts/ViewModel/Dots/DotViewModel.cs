using System;
using System.Linq;
using DotsCore;
using Model;
using Services;
using Services.Configuration;

namespace ViewModel.Dots
{
	public class DotViewModel : BaseViewModel
	{
		public event Action<DotViewModel> Pressed;
		public event Action<DotViewModel> PressFinished;

		public Dot DotData { get; }
		public float R { get; private set; }
		public float G { get; private set; }
		public float B { get; private set; }
		public Position Position { get; private set; }

		public DotViewModel(Dot dot)
		{
			DotData = dot;
		}

		public override void Initialize(GameModel gameModel, IServiceManager serviceManager)
		{
			base.Initialize(gameModel, serviceManager);
			
			var gameConfiguration = ServiceManager.GetService<IConfigurationService>().GameConfiguration;
			var dotEntities = gameConfiguration.BattleConfiguration.Dots;
			var dotEntity = dotEntities.First(entity => entity.Color.Name == DotData.Color.Name);
			
			R = dotEntity.Color.R;
			G = dotEntity.Color.G;
			B = dotEntity.Color.B;
		}

		public void Press()
		{
			Pressed?.Invoke(this);
		}

		public void PressFinish()
		{
			PressFinished?.Invoke(this);
		}

		public void SetDotPosition(Position position)
		{
			Position = position;
		}

		public void Destroy()
		{
			Pressed = null;
			PressFinished = null;
		}
	}
}