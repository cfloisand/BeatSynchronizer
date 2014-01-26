using UnityEngine;
using UnityEditor;
using System.Collections;
using SynchronizerData;

[CustomEditor(typeof(BeatCounter))]
public class BeatCounterEditor : Editor {

	private const string beatScalarTooltip = "This value acts as a multiplier for the beat value specified, allowing for beat counters to extend beyond " +
		"a single measure. It also affects the beat offset value. Range: 1-10";
	private const string negativeBeatOffsetTooltip = "Reverses the direction of the offset beat value so that the offset is behind the beat.";
	private const string loopTimeTooltip = "Controls the frequency that the counter checks for beats. In milliseconds.";


	public override void OnInspectorGUI ()
	{
		var targetObject = (BeatCounter)target;

		targetObject.beatValue = (BeatValue)EditorGUILayout.EnumPopup("Beat value", targetObject.beatValue);
		targetObject.beatScalar = EditorGUILayout.IntSlider(new GUIContent("Scalar", beatScalarTooltip), targetObject.beatScalar, 1, 10);
		EditorGUILayout.Space ();

		targetObject.beatOffset = (BeatValue)EditorGUILayout.EnumPopup("Offset", targetObject.beatOffset);
		targetObject.negativeBeatOffset = EditorGUILayout.Toggle (new GUIContent("Negative offset", negativeBeatOffsetTooltip), targetObject.negativeBeatOffset);
		EditorGUILayout.Space ();

		targetObject.beatType = (BeatType)EditorGUILayout.EnumPopup("Beat type", targetObject.beatType);
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
