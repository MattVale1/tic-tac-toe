using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class GameBoard : MonoBehaviour {

    #region VARIABLES - USE SUB-REGIONS 
    #region INSTANCE
    public static GameBoard Instance { get; private set; }
    #endregion
    #region GAME BOARD DATA
    private byte gridSize = 3; // 3 as default value.   
    private List<Tile> tiles = new();
    #endregion
    #region BOARD GENERATION
    public Transform gameBoardParent;
    public GameObject tileButton; // Used for interacting with the game board.
    private const int tileGap = 200; // Our UI tile is 200x200 pixels.
    #endregion
    #endregion


    #region SETUP
    private void Awake() {
		Instance = this;
	}
    #endregion

    #region GAME BOARD - GENERATION
    private void GenerateGameBoard() {
        for (byte x = 0; x < gridSize; x++) {
            for (byte y = 0; y < gridSize; y++) {
                GameObject newTile = Instantiate(tileButton, Vector3.zero, Quaternion.identity, gameBoardParent);
                newTile.transform.localPosition = new Vector2(
                                                                (x * tileGap) - (gridSize * tileGap) / 2 + 100, 
                                                                (y * tileGap) - (gridSize * tileGap) / 2 + 100);
                Tile t = newTile.GetComponent<Tile>();
                tiles.Add(t);
                newTile.GetComponent<Button>().onClick.AddListener( () => TileClicked(t) );
            }
        }

        Vector3 newBoardScale = gameBoardParent.localScale;
        newBoardScale.x -= (float)gridSize/15;
        newBoardScale.y -= (float)gridSize/15;
        gameBoardParent.localScale = newBoardScale;
    }
    #endregion

    #region GAME BOARD - STATE UPDATES
    private bool CheckForWinner(Tile fromTile) {




        return false;
    }
    #endregion

    #region PUBLIC METHODS
    /// <summary>
    /// This should be called before generation, setting the grid size. Defaulted to the standard size of 3.
    /// </summary>
    /// <param name="size">Size of the game board in context of int x by int x.</param>
    public void SetGridSize(byte size = 3) {
        gridSize = size;
    }
    // This should be called when the player clicks the START button to init the board with a custom size.
    public void InitGameBoard() {
        GenerateGameBoard();
    }

    // Handles the clicking of a tile button (player places an X or O).
    public void TileClicked(Tile tile) {
        // Change internal state of the tile and tell it to change the visual.
        tile.SetSymbol(GameStateController.Instance.curPlayerTurn);

        // Check for winner
        if (CheckForWinner(tile)) {

            return;
        }

        // If there's no winner yet, tell GameStateController to go to next player turn
        GameStateController.Instance.NextPlayerTurn();
    }
    #endregion

}