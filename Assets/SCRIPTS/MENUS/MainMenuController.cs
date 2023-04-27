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


    #region PUBLIC METHODS
    // We are playing solo, make sure GameStateController knows we need to use an AI as the opponent...
    public void Select_1_Player() {
        GameStateController.Instance.SetNumberOfPlayers(1, 1);
        //GameStateController.Instance.SetOpponentType(PlayerType.Type.COMPUTER, 2);
		UIController.Instance.OpenGameSetupCanvas();
	}

    // We are playing with a friend, make sure GameStateController knows we will be recieving human input for both players...
    public void Select_2_Player() {
        GameStateController.Instance.SetNumberOfPlayers(2, 0);
        //GameStateController.Instance.SetOpponentType(PlayerType.Type.HUMAN, 2);
        UIController.Instance.OpenGameSetupCanvas();
    }

    // Set the size of the game board using our slider.
    public void SetGridSize() {
        // Out slider is set to integer values via th checkbox, we can just use a cast here to enforce an integer.
        GameBoard.Instance.SetGridSize((int)gridSizeSlider.value); 
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
        GameStateController.Instance.RemoveAllPlayerInstances();
        UIController.Instance.ReturnToMainMenuCanvas();        
    }

    public void QuitGame() {
        Application.Quit();
    }
    #endregion

}