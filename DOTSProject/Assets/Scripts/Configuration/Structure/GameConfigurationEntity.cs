using Configuration.Structure.Scenes;

namespace Configuration.Structure
{
	public class GameConfigurationEntity
	{
		public SceneEntity Main { get; }

		public GameConfigurationEntity(SceneEntity mainSceneEntity)
		{
			Main = mainSceneEntity;
		}
	}
}