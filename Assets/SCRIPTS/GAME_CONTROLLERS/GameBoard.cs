using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour {

    #region VARIABLES - USE SUB-REGIONS 
    #region INSTANCE
    public static GameBoard Instance { get; private set; }
    #endregion
    #region GAME BOARD DATA
    private byte gridSize = 3; // 3 as default value.   
    private Tile[,] tilesOnBoard;
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
        tilesOnBoard = new Tile[gridSize, gridSize];
        for (byte x = 0; x < gridSize; x++) {
            for (byte y = 0; y < gridSize; y++) {
                GameObject newTile = Instantiate(tileButton, Vector3.zero, Quaternion.identity, gameBoardParent);
                newTile.transform.localPosition = new Vector2(
                                                                (x * tileGap) - (gridSize * tileGap) / 2 + 100, 
                                                                (y * tileGap) - (gridSize * tileGap) / 2 + 100);
                Tile t = newTile.GetComponent<Tile>();
                tilesOnBoard[x, y] = t;
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
    private bool CheckForWinner() {
        int matchingTileCount; // Variable to check if we have enough matching tiles to win.

        // Vertical check
        for (int x = 0; x < gridSize; x++) {
            matchingTileCount = 0; // Reset the counter per column so we don't include any previous column matches.
            for (int y = 0; y < gridSize - 1; y++) {
                //Debug.Log("Checking: " + x + " - " + y);
                if (!CompareTile(tilesOnBoard[x, y], tilesOnBoard[x, y + 1])) {                    
                    continue;
                }
                matchingTileCount++;
                if (matchingTileCount == gridSize - 1) {
                    return true;
                }
            }
        }

        // Horizontal check
        for (int y = 0; y < gridSize; y++) {
            matchingTileCount = 0; // Reset the counter per row so we don't include any previous row matches.
            for (int x = 0; x < gridSize - 1; x++) {
                if (!CompareTile(tilesOnBoard[x, y], tilesOnBoard[x + 1, y])) {                    
                    continue;
                }
                matchingTileCount++;
                if (matchingTileCount == gridSize - 1) {
                    return true;
                }
            }
        }

        // Diagonal check : bottom-left > top-right
        matchingTileCount = 0; // Reset the counter per diagonal so we don't include any previous diagonal matches.
        for (int i = 0; i < gridSize - 1; i++) {            
            //Debug.Log("Checking: " + i + " - " + i + " with " + (i+1) + " - " + (i+1));
            if (!CompareTile(tilesOnBoard[i, i], tilesOnBoard[i + 1, i + 1])) {
                continue;
            }
            matchingTileCount++;
            if (matchingTileCount == gridSize - 1) {
                return true;
            }
        }
        // Diagonal check : top-left > bottom-right
        matchingTileCount = 0; // Reset the counter per diagonal so we don't include any previous diagonal matches.
        int n = 0;
        for (int i = gridSize - 1; i > 0; i--) {
            //Debug.Log("Checking: " + i + " - " + i + " with " + (i+1) + " - " + (i+1));
            if (!CompareTile(tilesOnBoard[n, i], tilesOnBoard[n + 1, i - 1])) {
                continue;
            }
            n++;
            matchingTileCount++;
            if (matchingTileCount == gridSize - 1) {
                return true;
            }
        }

        // If there are no matches, we don't have a winner
        return false;
    }

    private bool CheckForTie() {
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                // If we iterate over an non-occupied tile, it can't be a tie
                if (tilesOnBoard[x, y].occupyingPlayer == null) {
                    return false;
                }
            }
        }
        // If all tiles are occupied with no winner, must be a tie...
        return true;
    }

    private bool CompareTile(Tile tile1, Tile tile2) {
        // If our tile is empty, ignore this comparison, there is no match.
        if (tile1.occupyingPlayer == null || tile2.occupyingPlayer == null) return false;
        if (tile1.occupyingPlayer == tile2.occupyingPlayer) return true;
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

    /// <summary>
    /// Toggles all tile buttons so the player can/can't interact anymore.
    /// </summary>
    public void ToggleGameBoard(bool toggle) {
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                tilesOnBoard[x, y].AllowTileInteraction(toggle);      
            }
        }
    }

    /// <summary>
    /// Enable ONLY tiles that are empty, this will ensure the player can't click an occupied tile.
    /// </summary>
    public void EnableInteractionOnEmptyTiles() {
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                if (tilesOnBoard[x, y].occupyingPlayer == null) {
                    tilesOnBoard[x, y].AllowTileInteraction(true);
                }
            }
        }
    }

    /// <summary>
    ///  Reset the game board ready for a new game.
    /// </summary>
    public void ResetBoard() {
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                tilesOnBoard[x, y].ResetTile();
            }
        }
    }

    // Handles the clicking of a tile button (player places an X or O).
    public void TileClicked(Tile tile) {
        // Change internal state of the tile and tell it to change the visual.
        tile.SetSymbol(GameStateController.Instance.curPlayerTurn);

        // Check for winner
        if (CheckForWinner()) {
            GameStateController.Instance.GameWon();
            return;
        }

        // Check for tie
        if (CheckForTie()) {
            GameStateController.Instance.GameTied();
            return;
        }

        // If there's no winner yet, tell GameStateController to go to next player turn
        GameStateController.Instance.NextPlayerTurn();
    }

    /// <summary>
    /// Get a random tile (used for AI)
    /// </summary>
    public Tile GetRandomTile() {
        byte randomRow = (byte)UnityEngine.Random.Range(0, gridSize);
        byte randomCol = (byte)UnityEngine.Random.Range(0, gridSize);

        while (tilesOnBoard[randomRow, randomCol].occupyingPlayer != null) {
            randomRow = (byte)UnityEngine.Random.Range(0, gridSize);
            randomCol = (byte)UnityEngine.Random.Range(0, gridSize);
        }

        //Debug.Log("<color=cyan>Computer found empty tile at: </color>" + randomRow + "-" + randomCol);
        return tilesOnBoard[randomRow, randomCol];
    }
    #endregion

}