using System;
using System.Linq;
using DotsCore;
using Services.Configuration;

namespace ViewModel.Dots
{
	public class DotViewModel : BaseViewModel
	{
		public event Action<DotViewModel, bool> PressStateChanged;

		public bool IsPressed
		{
			get => _isPressed;
			set
			{
				if (_isPressed != value)
				{
					PressStateChanged?.Invoke(this, value);

					_isPressed = value;
				}
			}
		}
		
		public Dot DotData { get; private set; }
		public float R { get; private set; }
		public float G { get; private set; }
		public float B { get; private set; }
		public Position Position { get; private set; }

		private bool _isPressed;

		public void SetDotInfo(Dot dot)
		{
			var gameConfiguration = ServiceManager.GetService<IConfigurationService>().GameConfiguration;
			var dotEntities = gameConfiguration.BattleConfiguration.Dots;
			var dotEntity = dotEntities.First(entity => entity.Color.Name == dot.Color.Name);

			DotData = dot;
			
			R = dotEntity.Color.R;
			G = dotEntity.Color.G;
			B = dotEntity.Color.B;
		}

		public void SetDotPosition(Position position)
		{
			Position = position;
		}

		public void SubscribeOnPressChangeEvent(Action<DotViewModel, bool> subscriber)
		{
			PressStateChanged += subscriber;
		}

		public void Destroy()
		{
			PressStateChanged = null;
		}
	}
}