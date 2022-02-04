using Configuration.Structure;
using UnityEngine;

namespace Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities
{
	[CreateAssetMenu(fileName = "Game Configuration", menuName = "Configuration/Main", order = 1)]
	public class GameConfigurationScriptableObjectEntity : ScriptableObject
	{
		[SerializeField] private SceneScriptableObjectEntity _mainScene = default;

		public GameConfiguration GetStructureData()
		{
			return new GameConfiguration(_mainScene.GetStructureData());
		}
	}
}