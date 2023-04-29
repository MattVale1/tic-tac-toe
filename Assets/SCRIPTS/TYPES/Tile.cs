using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

    public Button mButton;
    public Image mSprite;
    public Color emptyTileColor;
    public Player occupyingPlayer;

    public void SetSymbol(Player p) {
        mSprite.sprite = p.symbol;        
        mSprite.color = p.playerColor;
        mSprite.enabled = true;
        AllowTileInteraction(false);
        occupyingPlayer = p;
    }

    public void ResetTile() {
        mSprite.sprite = null;
        mSprite.color = emptyTileColor;
        mSprite.enabled = false;
        AllowTileInteraction(true);
        occupyingPlayer = null;
    }

    public void AllowTileInteraction(bool state) {
        mButton.interactable = state;
    }

}