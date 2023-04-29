/*-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+/
/-+ Script written by Matthew Vale, Red Phoenix Studios. -+-/
/-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+*/
using UnityEngine;
using UnityEngine.Playables;

public class AudioController : MonoBehaviour {

    #region VARIABLES - USE SUB-REGIONS 
    #region INSTANCE
    public static AudioController Instance { get; private set; }
    #endregion
    #region AUDIO SOURCES
    private AudioSource mAudioSource;
	public AudioClip buttonClick;
	public AudioClip gameWin;
	#endregion
	#endregion


	#region SETUP
	private void Awake() {
		Instance = this;
        mAudioSource= GetComponent<AudioSource>();
	}
    #endregion

    #region SOUND METHODS
    /// <summary>
    /// Play a specific audio clip with or without a random pitch adjustment.
    /// </summary>
    /// <param name="randPitchAmount">How much of a range to use for the random pitch.</param>
    private void PlayAudioClip(AudioClip clip, float randPitchAmount = 0f) {
        mAudioSource.clip = clip;
        mAudioSource.pitch = 1 + Random.Range(-randPitchAmount, randPitchAmount); // Make the click sound a little less repetitive.
        mAudioSource.Play();
    }
    #endregion

    #region PUBLIC METHODS    
    public void PlayButtonClick() {
        PlayAudioClip(buttonClick, 0.1f);
	}

	public void PlayGameWin() {
        PlayAudioClip(gameWin);
    }
	#endregion

}