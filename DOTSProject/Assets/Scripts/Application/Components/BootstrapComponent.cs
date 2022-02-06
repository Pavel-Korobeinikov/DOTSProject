using Application.Launcher;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Application.Components
{
	[RequireComponent(typeof(LaunchDataComponent))]
	public class BootstrapComponent : MonoBehaviour
	{
		[SerializeField] private LaunchDataComponent _launchData = default;
		
		private GameLauncher _gameLauncher;
		
		private void Awake()
		{
			var launchData = _launchData.GetLaunchData();
			_gameLauncher = new GameLauncher(launchData);
		}

		private async UniTaskVoid Start()
		{
			await _gameLauncher.Launch();
		}
	}
}