using UnityEngine;
using UnityEditor;
using System.Collections;
using SynchronizerData;

[CustomEditor(typeof(BeatCounter))]
public class BeatCounterEditor : Editor {

	public override void OnInspectorGUI ()
	{
		var targetObject = (BeatCounter)target;

		targetObject.beatValue = (BeatValue)EditorGUILayout.EnumPopup("Beat value", targetObject.beatValue);
		targetObject.beatScalar = EditorGUILayout.IntSlider("Scalar", targetObject.beatScalar, 1, 10);
		EditorGUILayout.Space ();

		targetObject.beatOffset = (BeatValue)EditorGUILayout.EnumPopup("Offset", targetObject.beatOffset);
		targetObject.negativeBeatOffset = EditorGUILayout.Toggle("Negative offset", targetObject.negativeBeatOffset);
		EditorGUILayout.Space ();

		targetObject.beatType = (BeatType)EditorGUILayout.EnumPopup("Beat type", targetObject.beatType);
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
