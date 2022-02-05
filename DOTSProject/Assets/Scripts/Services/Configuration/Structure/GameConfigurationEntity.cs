using Configuration.Structure.Scenes;

namespace Configuration.Structure
{
	public class GameConfigurationEntity
	{
		public SceneEntity MainScene { get; }

		public GameConfigurationEntity(SceneEntity mainSceneSceneEntity)
		{
			MainScene = mainSceneSceneEntity;
		}
	}
}