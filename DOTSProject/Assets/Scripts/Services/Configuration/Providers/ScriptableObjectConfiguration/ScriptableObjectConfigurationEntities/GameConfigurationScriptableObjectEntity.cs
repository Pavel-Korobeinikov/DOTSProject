using Services.Configuration.Structure;
using UnityEngine;

namespace Services.Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities
{
	[CreateAssetMenu(fileName = "Game Configuration", menuName = "Configuration/Main", order = 1)]
	public class GameConfigurationScriptableObjectEntity : ScriptableObject
	{
		[SerializeField] private SceneScriptableObjectEntity _mainScene = default;
		[SerializeField] private SceneScriptableObjectEntity _battleScene = default;

		public GameConfigurationEntity GetStructureData()
		{
			return new GameConfigurationEntity(
				_mainScene.GetStructureData(),
				_battleScene.GetStructureData());
		}
	}
}