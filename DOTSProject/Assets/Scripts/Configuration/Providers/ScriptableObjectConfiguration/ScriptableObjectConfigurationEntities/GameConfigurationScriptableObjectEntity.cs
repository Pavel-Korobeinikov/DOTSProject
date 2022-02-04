using Configuration.Structure;
using UnityEngine;

namespace Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities
{
	public class GameConfigurationScriptableObjectEntity : ScriptableObject
	{
		[SerializeField] private SceneScriptableObjectEntity _mainScene = default;

		public GameConfiguration GetStructureData()
		{
			return new GameConfiguration(_mainScene.GetStructureData());
		}
	}
}