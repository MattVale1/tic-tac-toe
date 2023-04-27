using UnityEngine;

public class GameBoard : MonoBehaviour {

    #region VARIABLES - USE SUB-REGIONS 
    #region INSTANCE
    public static GameBoard Instance { get; private set; }
    #endregion
    #region GAME BOARD DATA
    private int gridSize = 3; // 3 as default value.
    #endregion
    #region BOARD GENERATION
    public GameObject gridLine;  // Used to visually represent the game grid.
    public GameObject tileButton; // Used for interacting with the game board.
    #endregion
    #endregion


    #region SETUP
    private void Awake() {
		Instance = this;
	}
    #endregion

    #region GAME BOARD - GENERATION
    private void GenerateGameBoard() {

    }
    private void GenerateTileButtons() {

    }
    #endregion

    #region GAME BOARD - STATE UPDATES
    private void CheckForWinner(int fromTile) {

    }
    #endregion

    #region PUBLIC METHODS
    /// <summary>
    /// This should be called before generation, setting the grid size. Defaulted to the standard size of 3.
    /// </summary>
    /// <param name="size">Size of the game board in context of int x by int x.</param>
    public void SetGridSize(int size = 3) {
        gridSize = size;
    }
    // This should be called when the player clicks the START button to init the board with a custom size.
    public void InitGameBoard() {
        GenerateGameBoard();
    }

    // Handles the clicking of a tile button (player places an X or O).
    public void TileClicked() {

	}
	#endregion

}