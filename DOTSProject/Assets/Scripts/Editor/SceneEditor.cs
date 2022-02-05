using Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities;
using UnityEditor;

namespace Editor
{
	[CustomEditor(typeof(SceneScriptableObjectEntity), true)]
	public class SceneEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var scene = target as SceneScriptableObjectEntity;
			var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.ScenePath);

			serializedObject.Update();

			EditorGUI.BeginChangeCheck();
			var newScene = EditorGUILayout.ObjectField("Scene", oldScene, typeof(SceneAsset), false) as SceneAsset;

			if (EditorGUI.EndChangeCheck())
			{
				var newPath = AssetDatabase.GetAssetPath(newScene);
				var scenePathProperty = serializedObject.FindProperty("ScenePath");
				scenePathProperty.stringValue = newPath;
			}
			serializedObject.ApplyModifiedProperties();
			
			DrawDefaultInspector();
		}
	}
}