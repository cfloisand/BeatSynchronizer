using UnityEngine;
using System.Collections;
using SynchronizerData;

/// <summary>
/// This script needs to be added to any object that will observe a beat counter or pattern counter, and will receive notification
/// when a beat happens in the source audio through setting a bit mask whose bits correspond to specific beat types. The observing 
/// object should contain a script that polls for the current value of beatMask. When this value is non-zero, a beat has just fired. 
/// The beatWindow field specifies (in milliseconds) how long the beat stays active for, effectively behaving as a sensitivity/tolerance 
/// setting.
/// </summary>
/// <remarks>
/// By having individual beat observers attached to objects, finer control over sensitivity settings is granted to the client, which 
/// may contribute to lower CPU loads.
/// </remarks>
public class BeatObserver : MonoBehaviour {

	[Range(0, 500)]
	public float beatWindow = 10f;	// in milliseconds
	
	[HideInInspector]
	public BeatType beatMask;


	void Start ()
	{
		beatMask = BeatType.None;
	}

	/// <summary>
	/// This method is called by each BeatCounter and Pattern counter this object is observing.
	/// </summary>
	/// <param name="beatType">The beat type that invoked this method.</param>
	public void BeatNotify (BeatType beatType)
	{
		beatMask |= beatType;
		StartCoroutine(WaitOnBeat(beatType));
	}

	/// <summary>
	/// Clears the bit corresponding to the beat type after a specified duration of time.
	/// </summary>
	/// <param name="beatType">The beat type to clear.</param>
	IEnumerator WaitOnBeat (BeatType beatType)
	{
		yield return new WaitForSeconds(beatWindow / 1000f);
		beatMask ^= beatType;
	}

}
