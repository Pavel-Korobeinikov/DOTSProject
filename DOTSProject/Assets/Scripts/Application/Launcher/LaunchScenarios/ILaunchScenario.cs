using Cysharp.Threading.Tasks;

namespace Application.Launcher.LaunchScenarios
{
	public interface ILaunchScenario
	{
		UniTask Launch();
	}
}