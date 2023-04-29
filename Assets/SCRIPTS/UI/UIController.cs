using TMPro;
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
	#region MAIN CANVASES
	public Canvas mainMenuCanvas, gameSetupCanvas, gameCanvas, helpCanvas;
    #endregion
    #region AUDIO
    public Image audioImage;
	public Sprite audioIcon_On, audioIcon_Muted;
	#endregion
	#region GAME STATE UI
	public TextMeshProUGUI gameStateText;
	public TextMeshProUGUI playerTurnText;
	public TextMeshProUGUI player1ScoreText;
	public TextMeshProUGUI player2ScoreText;
	public TextMeshProUGUI timerText;
	public GameObject rematchButton;
    #endregion
    #endregion


    #region SETUP
    private void Awake() {
		Instance = this;
	}

    private void Start() {
		CloseAllUICanvases(mainMenuCanvas);    
    }
    #endregion

    #region UI STATE
    private void SwitchUIState(UIState state) {
		curUIState = state;
	}
    #endregion

    #region COMMON UI METHODS (private)
    /// <summary>
    /// Useful for disabling all canvases and enabling a new one, if required.
    /// </summary>
    /// <param name="canvasToOpen">An optional canvas to open after closing the others.</param>
    private void CloseAllUICanvases(Canvas canvasToOpen = null) {
		mainMenuCanvas.enabled = false;
		gameSetupCanvas.enabled = false;
		gameCanvas.enabled = false;
		helpCanvas.enabled = false;

		if (canvasToOpen != null)		canvasToOpen.enabled = true;

        rematchButton.SetActive(false);
		timerText.enabled = false;
    }
	#endregion

	#region PUBLIC METHODS - MENUS
	/// <summary>
	/// Set the audio icon to muted or not.
	/// </summary>
	public void SetMuteIcon(bool state) {
		if (state)	audioImage.sprite = audioIcon_Muted;
		if (!state)	audioImage.sprite = audioIcon_On;
	}

	/// <summary>
	/// Display the help canvas. Useful for players new to the game or those who need a reminder.
	/// </summary>
	public void ToggleHelpCanvas() {
		if (!helpCanvas.enabled) {
			SwitchUIState(UIState.HELP);
			helpCanvas.enabled = true;
		} else {
			helpCanvas.enabled = false;
		}
	}

	public void OpenGameSetupCanvas() {
		CloseAllUICanvases(gameSetupCanvas);
	}

	public void ReturnToMainMenuCanvas() {
		CloseAllUICanvases(mainMenuCanvas);
		SwitchUIState(UIState.MAIN_MENU);
	}

	public void OpenGameBoardCanvas() {
		CloseAllUICanvases(gameCanvas);
	}
    #endregion

    #region PUBLIC METHODS - GAME STATE
    public void UpdatePlayerTurn(Player p) {
		playerTurnText.text = p.playerName;
		playerTurnText.color = p.playerColor;
	}
	public void UpdateScore(Player p1, Player p2) {
		player1ScoreText.text = p1.playerName + ": " + p1.playerScore.ToString();
		player1ScoreText.color = p1.playerColor;
		player2ScoreText.text = p2.playerName + ": " + p2.playerScore.ToString();
        player2ScoreText.color = p2.playerColor;
    }
	public void UpdateGameStateText(string t) {
        gameStateText.text = t;
	}
	public void UpdateGameTimer(float timeRemaining) {
		if (!timerText.enabled) timerText.enabled = true;
		timerText.text = timeRemaining.ToString("F0");

    }
	public void OfferRematch() {
		rematchButton.SetActive(true);
	}
	public void HideRematchOffer() {
        rematchButton.SetActive(false);
    }
    #endregion

}