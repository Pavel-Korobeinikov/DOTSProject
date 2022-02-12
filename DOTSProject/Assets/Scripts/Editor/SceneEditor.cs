using Services.Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using View;

namespace Editor
{
	[CustomEditor(typeof(SceneScriptableObjectEntity), true)]
	public class SceneEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			var scenePathProperty = serializedObject.FindProperty("ScenePath");

			EditorGUI.BeginChangeCheck();
			var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePathProperty.stringValue);
			var newScene = EditorGUILayout.ObjectField("Scene", oldScene, typeof(SceneAsset), false) as SceneAsset;

			if (EditorGUI.EndChangeCheck())
			{
				var newPath = AssetDatabase.GetAssetPath(newScene);
				var sceneObject = SceneManager.GetSceneByPath(newPath);

				if (newScene == null || sceneObject.GetRootGameObjects()[0]?.GetComponent<ISceneView>() != null)
				{
					scenePathProperty.stringValue = newPath;
				}
				else if (newScene != null)
				{
					Debug.LogError($"Root gameObject in scene {newPath} doesn't contain IViewModel");
				}
			}
			serializedObject.ApplyModifiedProperties();
			
			DrawDefaultInspector();
		}
	}
}