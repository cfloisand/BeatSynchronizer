using UnityEngine;
using System.Collections;
using SynchronizerData;

/// <summary>
/// This class is responsible for counting and notifying its observers when a beat occurs, specified by beatValue.
/// An offset beat value can be set to shift the beat (e.g. to create syncopation). If the offset is negative, it shifts to the left (behind the beat).
/// The accuracy of the beat counter is handled by loopTime, which controls how often it checks whether a beat has happened.
/// Higher settings for loopTime decreases load on the CPU, but will result in less accurate beat synchronization.
/// </summary>
public class BeatCounter : MonoBehaviour {
	
	public BeatValue beatValue = BeatValue.QuarterBeat;
	public int beatScalar = 1;
	public BeatValue beatOffset = BeatValue.None;
	public bool negativeBeatOffset = false;
	public BeatType beatType = BeatType.OnBeat;
	public float loopTime = 30f;
	public AudioSource audioSource;
	public GameObject[] observers;
	
	private float nextBeatSample;
	private float samplePeriod;
	private float sampleOffset;
	private float currentSample;

	
	void Awake ()
	{
		// Calculate number of samples between each beat.
		float audioBpm = audioSource.GetComponent<BeatSynchronizer>().bpm;
		samplePeriod = (60f / (audioBpm * BeatDecimalValues.values[(int)beatValue])) * audioSource.clip.frequency;

		if (beatOffset != BeatValue.None) {
			sampleOffset = (60f / (audioBpm * BeatDecimalValues.values[(int)beatOffset])) * audioSource.clip.frequency;
			if (negativeBeatOffset) {
				sampleOffset = samplePeriod - sampleOffset;
			}
		}

		samplePeriod *= beatScalar;
		sampleOffset *= beatScalar;
		nextBeatSample = 0f;
	}

	/// <summary>
	/// Initializes and starts the coroutine that checks for beat occurrences. The nextBeatSample field is initialized to 
	/// exactly match up with the sample that corresponds to the time the audioSource clip started playing (via PlayScheduled).
	/// </summary>
	/// <param name="syncTime">Equal to the audio system's dsp time plus the specified delay time.</param>
	void StartBeatCheck (double syncTime)
	{
		nextBeatSample = (float)syncTime * audioSource.clip.frequency;
		StartCoroutine(BeatCheck());
	}
	
	/// <summary>
	/// Subscribe the BeatCheck() coroutine to the beat synchronizer's event.
	/// </summary>
	void OnEnable ()
	{
		BeatSynchronizer.OnAudioStart += StartBeatCheck;
	}

	/// <summary>
	/// Unsubscribe the BeatCheck() coroutine from the beat synchronizer's event.
	/// </summary>
	/// <remarks>
	/// This should NOT (and does not) call StopCoroutine. It simply removes the function that was added to the
	/// event delegate in OnEnable().
	/// </remarks>
	void OnDisable ()
	{
		BeatSynchronizer.OnAudioStart -= StartBeatCheck;
	}

	/// <summary>
	/// This method checks if a beat has occurred in the audio by comparing the current sample position of the audio system's dsp time 
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
			currentSample = (float)AudioSettings.dspTime * audioSource.clip.frequency;
			
			if (currentSample >= (nextBeatSample + sampleOffset)) {
				foreach (GameObject obj in observers) {
					obj.GetComponent<BeatObserver>().BeatNotify(beatType);
				}
				nextBeatSample += samplePeriod;
			}

			yield return new WaitForSeconds(loopTime / 1000f);
		}
	}

}
