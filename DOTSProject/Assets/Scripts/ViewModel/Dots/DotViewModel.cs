using System;
using System.Linq;
using DotsCore;
using Services.Configuration;

namespace ViewModel.Dots
{
	public class DotViewModel : BaseViewModel
	{
		public event Action<DotViewModel> DotPressed; 
		public event Action<DotViewModel> DotPressEnded;
		
		public float R { get; private set; }
		public float G { get; private set; }
		public float B { get; private set; }

		public int X { get; private set; }
		public int Y { get; private set; }

		public void SetDotInfo(Dot dot)
		{
			var gameConfiguration = _serviceManager.GetService<IConfigurationService>().GameConfiguration;
			var dotEntities = gameConfiguration.BattleConfiguration.Dots;
			var dotEntity = dotEntities.First(entity => entity.Color.Name == dot.Color.Name);

			R = dotEntity.Color.R;
			G = dotEntity.Color.G;
			B = dotEntity.Color.B;

			X = dot.Position.X;
			Y = dot.Position.Y;
		}

		public void SubscribeOnPressEvent(Action<DotViewModel> subscriber)
		{
			DotPressed += subscriber;
		}

		public void SubscribeOnPressEndedEvent(Action<DotViewModel> subscriber)
		{
			DotPressEnded += subscriber;
		}

		public void Press()
		{
			DotPressed?.Invoke(this);
		}

		public void FinishPress()
		{
			DotPressEnded?.Invoke(this);
		}

		public void Destroy()
		{
			DotPressed = null;
			DotPressEnded = null;
		}
	}
}