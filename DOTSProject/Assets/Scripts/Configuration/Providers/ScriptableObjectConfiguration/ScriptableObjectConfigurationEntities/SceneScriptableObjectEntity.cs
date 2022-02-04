using Configuration.Structure.Scenes;
using UnityEngine;

namespace Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities
{
	public class SceneScriptableObjectEntity : ScriptableObject
	{
		public string AssetPath = default;

		public Scene GetStructureData()
		{
			return new Scene(AssetPath);
		}
	}
}