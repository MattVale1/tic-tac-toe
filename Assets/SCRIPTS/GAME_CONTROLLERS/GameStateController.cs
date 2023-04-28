using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour {

    #region VARIABLES - USE SUB-REGIONS 
    #region INSTANCE
    public static GameStateController Instance { get; private set; }
	#endregion
	#region GAME STATE
	private GameState.State curState = GameState.State.ONGOING; // Ongoing by default.
	#endregion
	#region GAME CONFIG
	private GameMode.Modes curMode = GameMode.Modes.STANDARD; // Store our currently selected game mode, standard by default.
    #endregion
    #region PLAYERS
    private PlayerType.Type opponent = PlayerType.Type.COMPUTER; // Computer by default.
    public Player curPlayerTurn { get; private set; }
    private Player player1;
    private Player player2;
    #endregion
    #region PLAYER SYMBOLS
    [Header("Player Colours")]
    public Sprite player1Sprite;
    public Color player1Color;
    public Sprite player2Sprite;
    public Color player2Color;
    #endregion
    #endregion


    #region SETUP
    private void Awake() {
		Instance = this;
	}
    #endregion


    #region GAME LOGIC	
    public void NextPlayerTurn() {
        // If we have a null reference, always enforce player1 as being the first to play.
        if (curPlayerTurn == null) {
            curPlayerTurn = player1;
            return;
        }
        // This will switch the current player, always being the opposite.
        curPlayerTurn = (curPlayerTurn == player1) ? player2 : player1;

        // Update our UI to reflect current player
        UIController.Instance.UpdatePlayerTurn(curPlayerTurn);
	}

	private void CheckGameState() {

	}
    #endregion

    #region PUBLIC METHODS
    public void SetOpponent(PlayerType.Type opponentType) {
        opponent = opponentType;
    }

	// Set our game mode via the dropdown menu. Take note of the index numbers, they should match the number entered on the button.
	public void SetGameMode(int modeIndex) {
		switch (modeIndex) {
			case 0:	curMode = GameMode.Modes.STANDARD;	break;
			case 1:	curMode = GameMode.Modes.TIME_LIMIT;	break;
			case 2:	curMode = GameMode.Modes.YOU_VS_THEM;	break;
        }
	}

    /// <summary>
    /// Generate the game board, allow game board interaction and kick off the game process.
    /// </summary>
    public void StartGame() {
        GameBoard.Instance.InitGameBoard();

        // Create new player instances with an initial score, type, name, symbol and colour.
        player1 = new Player(0, PlayerType.Type.HUMAN, "PLAYER 1", player1Sprite, player1Color);
        player2 = new Player(0, opponent, "PLAYER 2", player2Sprite, player2Color);

        NextPlayerTurn();
    }

    /// <summary>
    /// End the current game without declaring a winner. We can just reload the scene for ease.
    /// </summary>
    public void EndGameAndReturnToMenu() {
        SceneManager.LoadScene("MainScene");
    }
    #endregion

}