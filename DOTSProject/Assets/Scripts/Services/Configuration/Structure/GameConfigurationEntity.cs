using Services.Configuration.Structure.Scenes;

namespace Services.Configuration.Structure
{
	public class GameConfigurationEntity
	{
		public SceneEntity MainScene { get; }
		public SceneEntity BattleScene { get; }

		public GameConfigurationEntity(
			SceneEntity mainSceneEntity,
			SceneEntity battleSceneEntity)
		{
			MainScene = mainSceneEntity;
			BattleScene = battleSceneEntity;
		}
	}
}