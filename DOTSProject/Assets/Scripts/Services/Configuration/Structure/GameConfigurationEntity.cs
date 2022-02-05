using Services.Configuration.Structure.Scenes;

namespace Services.Configuration.Structure
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