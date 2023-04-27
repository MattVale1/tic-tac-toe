using UnityEngine;

public class GameStateController : MonoBehaviour {

    #region VARIABLES - USE SUB-REGIONS 
    #region INSTANCE
    public static GameStateController Instance { get; private set; }
	#endregion
	#region PLAYER TYPES
	private PlayerType opponentType;
	public enum PlayerType {
		HUMAN, COMPUTER
	}
    #endregion
    #endregion


    #region SETUP
    private void Awake() {
		Instance = this;
		Init();
	}
	private void Init() {
		CacheVars();
	}
	private void CacheVars() {
	
	}
	private void Start(){
		
	}
	#endregion

	#region PUBLIC METHODS
	public void SetOpponentType(PlayerType type) {
		opponentType = type;
	}
	#endregion

}