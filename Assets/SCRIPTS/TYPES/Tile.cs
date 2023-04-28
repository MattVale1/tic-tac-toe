using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

    public Button mButton;
    public Image mSprite;
    public Color emptyTileColor;

    public void SetSymbol(Player p) {
        mSprite.sprite = p.symbol;        
        mSprite.color = p.playerColor;
        mSprite.enabled = true;
        mButton.interactable = false;
    }

    public void ResetTile() {
        mSprite.sprite = null;
        mSprite.color = emptyTileColor;
        mSprite.enabled = false;
        mButton.interactable = true;        
    }

}