using UnityEngine;

namespace Application.Components
{
	public class BootstrapComponent : MonoBehaviour
	{
		[SerializeField] private LaunchDataComponent _launchData = default;
		
		private GameLauncher _gameLauncher;
		
		private void Awake()
		{
			var launchData = _launchData.GetLaunchData();
			_gameLauncher = new GameLauncher(launchData);
		}

		private async void Start()
		{
			await _gameLauncher.Launch();
		}
	}
}