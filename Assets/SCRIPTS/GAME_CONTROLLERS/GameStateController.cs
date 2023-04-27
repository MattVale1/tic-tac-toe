using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    private PlayerType.Type opponentType = PlayerType.Type.COMPUTER; // Computer by default.
    private Player curPlayerForTurn;
    public List<Player> curPlayers = new(); // List of players currently in the game.
    #endregion
    #endregion


    #region SETUP
    private void Awake() {
		Instance = this;
	}

    private void CreatePlayerInstance(PlayerType.Type type) {
        Debug.Log("Creating player instance of type: " + type);
    }    
    #endregion


    #region GAME LOGIC	
    private void NextPlayerTurn() {
        for (int i = 0; i < curPlayers.Count; i++) {
            
        }
	}

	private void CheckGameState() {

	}
    #endregion

    #region PUBLIC METHODS
    public void SetNumberOfPlayers(int humans, int computers) {
        for (int h = 0; h < humans; h++) {
            CreatePlayerInstance(PlayerType.Type.HUMAN);
        }
        for (int c = 0; c < computers; c++) {
            CreatePlayerInstance(PlayerType.Type.COMPUTER);
        }
    }
    public void RemoveAllPlayerInstances() {
        Debug.Log("Removing all player instances");
        curPlayers.Clear();
    }
 //   public void SetOpponentType(PlayerType.Type type, int playersInvoled) {
	//	opponentType = type;
	//}

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
    }
    #endregion

}