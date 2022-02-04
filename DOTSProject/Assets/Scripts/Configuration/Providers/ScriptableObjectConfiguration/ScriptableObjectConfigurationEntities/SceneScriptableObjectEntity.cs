using Configuration.Structure.Scenes;
using UnityEngine;

namespace Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities
{
	[CreateAssetMenu(fileName = "Scene Configuration", menuName = "Configuration/Scene", order = 2)]
	public class SceneScriptableObjectEntity : ScriptableObject
	{
		public string AssetPath = default;

		public Scene GetStructureData()
		{
			return new Scene(AssetPath);
		}
	}
}