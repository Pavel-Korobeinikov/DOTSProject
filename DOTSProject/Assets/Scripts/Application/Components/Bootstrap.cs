using UnityEngine;

namespace Application.Components
{
	public class Bootstrap : MonoBehaviour
	{
		private GameLauncher _gameLauncher;
		
		private void Awake()
		{
			_gameLauncher = new GameLauncher();
		}

		private async void Start()
		{
			await _gameLauncher.Launch();
		}
	}
}