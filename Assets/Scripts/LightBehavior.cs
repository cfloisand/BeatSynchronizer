using UnityEngine;
using System.Collections;
using SynchronizerData;


public class LightBehavior : MonoBehaviour {

	[Range(0f, 500f)]
	public float blinkDuration = 30f;	// milliseconds

	private BeatObserver beatObserver;


	void Start ()
	{
		beatObserver = GetComponent<BeatObserver>();
		light.enabled = false;
	}

	void Update ()
	{
		if ((beatObserver.beatMask & BeatType.OffBeat) == BeatType.OffBeat) {
			StartCoroutine(LightBlink());
		}
	}

	IEnumerator LightBlink ()
	{
		light.enabled = true;
		yield return new WaitForSeconds(blinkDuration / 1000f);
		light.enabled = false;
	}
}
