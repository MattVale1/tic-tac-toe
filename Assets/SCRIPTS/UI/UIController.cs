using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	#region VARIABLES - USE SUB-REGIONS 
	#region INSTANCE
	public static UIController Instance { get; private set; }
	#endregion
	#region UI STATE
	public UIState curUIState { get; private set; }
	public enum UIState {
		MAIN_MENU, HELP, GAME
	}
    #endregion
    #region AUDIO
    public Image audioImage;
	public Sprite audioIcon_On, audioIcon_Muted;
	#endregion
	#region HELP
	public Canvas helpCanvas;
    #endregion
    #endregion


    #region SETUP
    private void Awake() {
		Instance = this;
	}
    #endregion

    #region UI STATE
	private void SwitchUIState(UIState state) {
		curUIState = state;
	}
    #endregion

    #region PUBLIC METHODS
    /// <summary>
    /// Set the audio icon to muted or not.
    /// </summary>
    public void SetMuteIcon(bool state) {
		if (state)	audioImage.sprite = audioIcon_Muted;
		if (!state)	audioImage.sprite = audioIcon_On;
	}

	public void ToggleHelpCanvas() {
		if (!helpCanvas.enabled) {
			SwitchUIState(UIState.HELP);
			helpCanvas.enabled = true;
		} else {
			helpCanvas.enabled = false;
		}
	}
	#endregion

}