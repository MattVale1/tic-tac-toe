using UnityEngine;

public class Player {

    public int playerScore;
    public PlayerType.Type playerType;
    public string playerName;
    public Sprite symbol;
    public Color playerColor;

    /// <summary>
    /// Create a new instance of a player with some parameters.
    /// </summary>
    /// <param name="initialScore">Typically '0', but can be used to give starting points or even load from a save file.</param>
    /// <param name="playerType">Type of player, human or AI.</param>
    /// <param name="playerName">Name given to the player.</param>
    /// <param name="symbol">Visual sprite given to represent the player.</param>
    /// <param name="playerColor">The colour given to represent the player.</param>
    public Player(int initialScore, PlayerType.Type playerType, string playerName, Sprite symbol, Color playerColor) {
        playerScore = initialScore;
        this.playerType = playerType;
        this.playerName = playerName;
        this.symbol = symbol;
        this.playerColor = playerColor;
    }

}