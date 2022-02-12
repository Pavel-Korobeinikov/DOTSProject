using Services.Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities.Battle;
using Services.Configuration.Structure;
using UnityEngine;

namespace Services.Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities
{
	[CreateAssetMenu(fileName = "Game Configuration", menuName = "Configuration/Main", order = 1)]
	public class GameConfigurationScriptableObjectEntity : ScriptableObject
	{
		[SerializeField] private SceneScriptableObjectEntity _mainScene = default;
		[SerializeField] private SceneScriptableObjectEntity _dotsScene = default;
		[SerializeField] private BattleScriptableObjectEntity _battleConfiguration = default;

		public GameConfigurationEntity GetStructureData()
		{
			return new GameConfigurationEntity(
				_mainScene.GetStructureData(),
				_dotsScene.GetStructureData(),
				_battleConfiguration.GetStructureData());
		}
	}
}