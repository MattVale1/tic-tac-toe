public class Player {

    public int playerScore;
    public PlayerType.Type playerType;
    public string playerName;

    /// <summary>
    /// Create a new instance of a player with some parameters.
    /// </summary>
    /// <param name="initialScore">Typically '0', but can be used to give starting points or even load from a save file.</param>
    /// <param name="playerType">Type of player, human or AI.</param>
    /// <param name="playerName">Name given to the player.</param>
    public Player(int initialScore, PlayerType.Type playerType, string playerName) {
        playerScore = initialScore;
        this.playerType = playerType;
        this.playerName = playerName;
    }

}