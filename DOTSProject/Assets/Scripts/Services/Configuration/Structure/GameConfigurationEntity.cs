using Services.Configuration.Structure.Battle;

namespace Services.Configuration.Structure
{
	public class GameConfigurationEntity
	{
		public SceneEntity MainScene { get; }
		public SceneEntity DotsScene { get; }
		public BattleEntity BattleConfiguration { get; }

		public GameConfigurationEntity(
			SceneEntity mainSceneEntity,
			SceneEntity dotsSceneEntity,
			BattleEntity battleConfiguration)
		{
			MainScene = mainSceneEntity;
			DotsScene = dotsSceneEntity;
			BattleConfiguration = battleConfiguration;
		}
	}
}