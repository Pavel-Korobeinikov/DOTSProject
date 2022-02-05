using System.Collections.Generic;

namespace Services.Configuration.Structure.Scenes
{
	public class SceneEntity
	{
		public string ScenePath { get; }
		public List<SceneEntity> SceneDependencies { get; }

		public SceneEntity(
			string scenePath,
			List<SceneEntity> sceneDependencies)
		{
			ScenePath = scenePath;
			SceneDependencies = sceneDependencies;
		}
	}
}