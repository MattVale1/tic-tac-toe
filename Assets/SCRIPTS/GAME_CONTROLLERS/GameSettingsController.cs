using UnityEngine;

/// <summary>
/// For a game this size, this class, in its current state, could be moved into the UIController.cs ...
/// ... however if we were to expand this with graphics settings, it would be better to keep this here ...
/// ... and let UIController handle the UI elements.
/// </summary>
public class GameSettingsController : MonoBehaviour {


	#region PUBLIC METHODS
	public void ToggleMute() {
		if (AudioListener.volume != 0) {
            AudioListener.volume = 0f;
			UIController.Instance.SetMuteIcon(true);
		} else {
            AudioListener.volume = 1f;
            UIController.Instance.SetMuteIcon(false);
        }		
	}

	/// <summary>
	/// Toggle the help panel for new players.
	/// </summary>
	public void ToggleHelp() {
		UIController.Instance.ToggleHelpCanvas();
	}	
	#endregion

}