using System.Collections.Generic;
using System.Linq;
using Services.Configuration.Structure;
using UnityEngine;

namespace Services.Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities
{
	[CreateAssetMenu(fileName = "Scene Configuration", menuName = "Configuration/Scene", order = 2)]
	public class SceneScriptableObjectEntity : ScriptableObject
	{
		[HideInInspector] [SerializeField] private string _scenePath = default;
		[SerializeField] private List<SceneScriptableObjectEntity> _sceneDependencies = default;

		public SceneEntity GetStructureData()
		{
			return new SceneEntity(
				_scenePath,
				_sceneDependencies.Select(dependency => dependency.GetStructureData()).ToList());
		}
	}
}