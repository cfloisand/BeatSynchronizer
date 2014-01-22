using UnityEngine;
using UnityEditor;
using System.Collections;
using SynchronizerData;

[CustomEditor(typeof(PatternCounter))]
public class PatternCounterEditor : Editor {

	public override void OnInspectorGUI ()
	{
		var targetObject = (PatternCounter)target;

		serializedObject.Update();
		//EditorGUIUtility.LookLikeInspector();
		SerializedProperty bvs = serializedObject.FindProperty("beatValues");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(bvs, true);
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		//EditorGUIUtility.LookLikeControls();
		EditorGUILayout.Space ();

		targetObject.loopTime = EditorGUILayout.Slider("Loop time", targetObject.loopTime, 0f, 500f);
		targetObject.audioSource = (AudioSource)EditorGUILayout.ObjectField("Audio source", targetObject.audioSource, typeof(AudioSource), true);
		
		serializedObject.Update();
		//EditorGUIUtility.LookLikeInspector();
		SerializedProperty obs = serializedObject.FindProperty("observers");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(obs, true);
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		//EditorGUIUtility.LookLikeControls();
	}

}
