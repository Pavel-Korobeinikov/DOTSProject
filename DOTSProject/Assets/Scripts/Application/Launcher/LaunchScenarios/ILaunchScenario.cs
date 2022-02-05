using Cysharp.Threading.Tasks;

namespace Application.Launcher.LaunchScenaries
{
	public interface ILaunchScenario
	{
		UniTask Launch();
	}
}