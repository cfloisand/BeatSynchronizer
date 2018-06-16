using UnityEngine;
using System.Collections;
using SynchronizerData;

public class LightBehavior1 : MonoBehaviour {

	public Color[] colorSequence;

	private BeatObserver beatObserver;
	private int beatCounter;
	
	void Start ()
	{
		beatObserver = GetComponent<BeatObserver>();
		beatCounter = 0;
	}
	

	void Update ()
	{
		if ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat) {
			GetComponent<Light>().color = colorSequence[beatCounter];
			beatCounter = (++beatCounter == colorSequence.Length ? 0 : beatCounter);
		}
	}
}
