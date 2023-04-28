using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    #region VARIABLES - USE SUB-REGIONS 
    #region GAME CONFIGURATION OPTIONS
    public Slider gridSizeSlider;
    public TMP_Dropdown gameModeDropdown;
    #endregion
    #endregion


    #region SETUP
    private void Start() {
        gridSizeSlider.value = gridSizeSlider.minValue; // Set default value to the lowest (3).
    }
    #endregion


    #region PUBLIC METHODS
    // We are playing solo, make sure GameStateController knows we need to use an AI as the opponent...
    public void Select_1_Player() {
        GameStateController.Instance.SetOpponent(PlayerType.Type.COMPUTER);
		UIController.Instance.OpenGameSetupCanvas();
	}

    // We are playing with a friend, make sure GameStateController knows we will be recieving human input for both players...
    public void Select_2_Player() {
        GameStateController.Instance.SetOpponent(PlayerType.Type.HUMAN);
        UIController.Instance.OpenGameSetupCanvas();
    }

    // Set the size of the game board using our slider.
    public void SetGridSize() {
        // Out slider is set to integer values via the checkbox...
        // ... we can use a cast here to enforce a byte, as the intended size will range from 3-8.
        GameBoard.Instance.SetGridSize((byte)gridSizeSlider.value); 
    }

    // Set the game mode using our dropdown menu.
    public void SetGameMode() {
        GameStateController.Instance.SetGameMode(gameModeDropdown.value);
    }

    // Begin the game by opening the game canvas.
    public void StartGame() {
        GameStateController.Instance.StartGame();
        UIController.Instance.OpenGameBoardCanvas();
    }

    // Close game config and return to the main menu canvas.
	public void CloseGameSetup() {
        UIController.Instance.ReturnToMainMenuCanvas();        
    }

    public void QuitGame() {
        Application.Quit();
    }
    #endregion

}