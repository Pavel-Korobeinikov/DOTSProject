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
			var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.AssetPath);

			serializedObject.Update();

			EditorGUI.BeginChangeCheck();
			var newScene = EditorGUILayout.ObjectField("scene", oldScene, typeof(SceneAsset), false) as SceneAsset;

			if (EditorGUI.EndChangeCheck())
			{
				var newPath = AssetDatabase.GetAssetPath(newScene);
				var scenePathProperty = serializedObject.FindProperty("scenePath");
				scenePathProperty.stringValue = newPath;
			}
			serializedObject.ApplyModifiedProperties();
		}
	}
}