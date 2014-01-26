using UnityEngine;
using UnityEditor;
using System.Collections;
using SynchronizerData;

[CustomEditor(typeof(PatternCounter))]
public class PatternCounterEditor : Editor {
	
	private const string beatScalarTooltip = "This value acts as a multiplier for all the beat value specified in the pattern, allowing for the " +
		"sequence to extend beyond a single measure. Range: 1-10";
	private const string loopTimeTooltip = "Controls the frequency that the counter checks for beats. In milliseconds.";


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
		targetObject.beatScalar = EditorGUILayout.IntSlider(new GUIContent("Scalar", beatScalarTooltip), targetObject.beatScalar, 1, 10);
		EditorGUILayout.Space ();

		targetObject.loopTime = EditorGUILayout.Slider(new GUIContent("Loop time", loopTimeTooltip), targetObject.loopTime, 0f, 500f);
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
