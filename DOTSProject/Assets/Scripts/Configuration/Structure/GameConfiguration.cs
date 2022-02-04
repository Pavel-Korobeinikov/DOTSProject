using Configuration.Structure.Scenes;

namespace Configuration.Structure
{
	public class GameConfiguration
	{
		public Scene MainScene { get; }

		public GameConfiguration(Scene mainScene)
		{
			MainScene = mainScene;
		}
	}
}