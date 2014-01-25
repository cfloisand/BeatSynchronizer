using UnityEngine;
using System.Collections;
using SynchronizerData;

public class ParticlesBehavior : MonoBehaviour {

	private BeatObserver beatObserver;
	private ParticleSystem particleBurst;

	
	void Start ()
	{
		beatObserver = GetComponent<BeatObserver>();
		particleBurst = GetComponent<ParticleSystem>();
	}
	

	void Update ()
	{
		if ((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat) {
			particleBurst.Play();
		}
	}
}
