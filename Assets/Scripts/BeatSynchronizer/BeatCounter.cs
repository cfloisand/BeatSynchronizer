using UnityEngine;
using System.Collections;
using SynchronizerData;

/// <summary>
/// This class is responsible for counting and notifying its observers when a beat occurs, specified by beatValue.
/// A syncopation value can be set to offset the beat (if reversed, the syncopation happens "behind" the beat).
/// The accuracy of the beat counter is handled by loopTime, which controls how often it checks whether a beat has happened.
/// Higher settings for loopTime decreases load on the CPU, but will result in less accurate beat synchronization.
/// </summary>
public class BeatCounter : MonoBehaviour {
	
	public BeatValue beatValue = BeatValue.QuarterBeat;
	public BeatValue syncopation = BeatValue.None;
	public bool behindBeat = false;
	public BeatType beatType = BeatType.NormalBeat;
	public float loopTime = 30f;
	public AudioSource audioSource;
	public GameObject[] observers;
	
	private float nextBeatSample;
	private float samplePeriod;
	private float syncopationOffset;
	private float currentSample;
	private float previousSample;

	
	void Awake ()
	{
		// Calculate number of samples between each beat.
		float audioBpm = audioSource.GetComponent<BeatSynchronizer>().bpm;
		samplePeriod = (60f / (audioBpm * BeatDecimalValues.values[(int)beatValue])) * audioSource.clip.frequency;

		if (syncopation != BeatValue.None) {
			syncopationOffset = (60f / (audioBpm * BeatDecimalValues.values[(int)syncopation])) * audioSource.clip.frequency;
			// Reverse direction of syncopation, effectively shifting it to behind the beat (i.e. pickups, etc.)
			if (behindBeat) {
				syncopationOffset = samplePeriod - syncopationOffset;
			}
		}

		nextBeatSample = 0f;
		previousSample = 0f;
	}
	
	/// <summary>
	/// Subscribe the BeatCheck() coroutine to the beat synchronizer's event.
	/// </summary>
	void OnEnable ()
	{
		BeatSynchronizer.OnAudioStart += () => { StartCoroutine(BeatCheck()); };
	}

	/// <summary>
	/// Unsubscribe the BeatCheck() coroutine from the beat synchronizer's event.
	/// </summary>
	/// <remarks>
	/// This should NOT (and does not) call StopCoroutine. It simply removes the lambda function that was added to the
	/// event delegate in OnEnable().
	/// </remarks>
	void OnDisable ()
	{
		BeatSynchronizer.OnAudioStart -= () => { StartCoroutine(BeatCheck()); };
	}

	/// <summary>
	/// This method checks if a beat has occurred in the audio by comparing the current sample position of the playhead 
	/// to the next expected sample value of the beat. The frequency of the checks is controlled by the loopTime field.
	/// </summary>
	/// <remarks>
	/// The WaitForSeconds() yield statement places the execution of the coroutine right after the Update() call, so by 
	/// setting the loopTime to 0, this method will update as frequently as Update(). If even greater accuracy is 
	/// required, WaitForSeconds() can be replaced by WaitForFixedUpdate(), which will place this coroutine's execution
	/// right after FixedUpdate().
	/// </remarks>
	IEnumerator BeatCheck ()
	{
		while (audioSource.isPlaying) {
			currentSample = audioSource.timeSamples;
			
			// Only reset the next beat sample counter when the audio clip has wrapped.
			// This is the simplest and most reliable way of doing this.
			if (currentSample < previousSample) {
				nextBeatSample = 0f;
			}
			
			if (currentSample >= (nextBeatSample + syncopationOffset)) {
				foreach (GameObject obj in observers) {
					obj.GetComponent<BeatObserver>().BeatNotify(beatType);
				}
				nextBeatSample += samplePeriod;
			}
			
			previousSample = currentSample;

			yield return new WaitForSeconds(loopTime / 1000f);
		}
	}

}
