using System.Collections.Generic;
using System.Linq;
using Configuration.Structure.Scenes;
using UnityEngine;

namespace Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities
{
	[CreateAssetMenu(fileName = "Scene Configuration", menuName = "Configuration/Scene", order = 2)]
	public class SceneScriptableObjectEntity : ScriptableObject
	{
		[HideInInspector] public string ScenePath = default;
		public List<SceneScriptableObjectEntity> SceneDependencies = default;

		public SceneEntity GetStructureData()
		{
			return new SceneEntity(
				ScenePath,
				SceneDependencies.Select(dependency => dependency.GetStructureData()).ToList());
		}
	}
}