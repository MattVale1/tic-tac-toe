using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour {

    #region VARIABLES - USE SUB-REGIONS 
    #region INSTANCE
    public static GameStateController Instance { get; private set; }
	#endregion
	#region GAME CONFIG
	private GameMode.Modes curMode = GameMode.Modes.STANDARD; // Store our currently selected game mode, standard by default.
    #endregion
    #region GAME STATE
    private GameState.State curState;
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
    #region TIMED MODE
    private float timeRemaining = 10f;
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

        if (curPlayerTurn.playerType == PlayerType.Type.COMPUTER) {
            // Disable all buttons while the computer makes a move
            GameBoard.Instance.ToggleGameBoard(false);
            StartCoroutine(ComputerMove());
        } else {
            GameBoard.Instance.EnableInteractionOnEmptyTiles();
        }
	}

    private IEnumerator ComputerMove() {
        yield return new WaitForSeconds(0.5f);
        // Automatically select a random tile for the computer
        GameBoard.Instance.TileClicked(GameBoard.Instance.GetRandomTile());
    }

    private void StartTimer() {
        timeRemaining = 10f;
        StartCoroutine(Timer());
    }
   
    private IEnumerator Timer() {
        UIController.Instance.UpdateGameTimer(timeRemaining);
        yield return new WaitForSeconds(1f);
        // If the game state changed during our countdown, we may want to stop the timer.
        if (curState == GameState.State.ENDED) yield break;
        timeRemaining -= 1f;        
        if (timeRemaining <= 0f) {
            EndTimedGame();
        } else {
            StartCoroutine(Timer());
        }
    }

    public void GameTied() {
        GameEnded("GAME TIED!");
    }
	
    public void GameWon() {
        //Debug.Log("<color=lime>Game won! Winner is: </color>" + curPlayerTurn.playerName);
        // Play a sound
        AudioController.Instance.PlayGameWin();
        // Update player score
        curPlayerTurn.playerScore++;
        // End the game and update the game state text to the following...
        GameEnded("THE WINNER IS...");
	}
    
    private void EndTimedGame() {
        GameEnded("TIME'S UP!");
    }
    
    /// <summary>
    /// Used for generic game ending events.
    /// </summary>
    private void GameEnded(string reason = null) {
        // Set game state
        curState = GameState.State.ENDED;
        // Disable all remaining buttons
        GameBoard.Instance.ToggleGameBoard(false);
        // Display winner
        UIController.Instance.UpdateGameStateText(reason);
        // Update scores
        UIController.Instance.UpdateScore(player1, player2);
        // Display replay button
        UIController.Instance.OfferRematch();
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

        curState = GameState.State.ONGOING;
        if (curMode == GameMode.Modes.TIME_LIMIT) {
            StartTimer();
        }
    }

    /// <summary>
    /// Begin a new match with the same players, keeping a tally of the score.
    /// </summary>
    public void Rematch() {
        // Disable replay button
        UIController.Instance.HideRematchOffer();
        GameBoard.Instance.ResetBoard();
        UIController.Instance.UpdateGameStateText("Your turn:");
        NextPlayerTurn();

        curState = GameState.State.ONGOING;
        if (curMode == GameMode.Modes.TIME_LIMIT) {
            StartTimer();
        }
    }

    /// <summary>
    /// End the current game without declaring a winner. We can just reload the scene for ease.
    /// </summary>
    public void EndGameAndReturnToMenu() {
        SceneManager.LoadScene("MainScene");
    }
    #endregion

}