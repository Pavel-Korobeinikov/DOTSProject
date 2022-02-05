using Application.Launcher;
using UnityEngine;

namespace Application.Components
{
	public class LaunchDataComponent : MonoBehaviour
	{
		[SerializeField] private string configurationPath = default;

		public LaunchData GetLaunchData()
		{
			return new LaunchData(configurationPath);
		}
	}
}