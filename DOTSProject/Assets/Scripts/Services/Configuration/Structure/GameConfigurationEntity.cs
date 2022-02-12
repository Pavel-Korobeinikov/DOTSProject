using Services.Configuration.Structure.Battle;

namespace Services.Configuration.Structure
{
	public class GameConfigurationEntity
	{
		public SceneEntity MainScene { get; }
		public SceneEntity BattleScene { get; }
		public BattleEntity BattleConfiguration { get; }

		public GameConfigurationEntity(
			SceneEntity mainSceneEntity,
			SceneEntity battleSceneEntity,
			BattleEntity battleConfiguration)
		{
			MainScene = mainSceneEntity;
			BattleScene = battleSceneEntity;
			BattleConfiguration = battleConfiguration;
		}
	}
}