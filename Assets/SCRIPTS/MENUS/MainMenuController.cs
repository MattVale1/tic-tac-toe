using UnityEngine;

public class MainMenuController : MonoBehaviour {

	#region VARIABLES - USE SUB-REGIONS 
	#region CACHE

	#endregion
	#endregion


	#region SETUP
	private void Awake() {
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
	public void Select_1_Player() {
		// We are playing solo, make sure GameStateController knows we need to use an AI as the opponent...
		GameStateController.Instance.SetOpponentType(GameStateController.PlayerType.COMPUTER);
		UIController.Instance.OpenGameSetupCanvas();
	}
    public void Select_2_Player() {
        // We are playing with a friend, make sure GameStateController knows we will be recieving human input for both players...
        GameStateController.Instance.SetOpponentType(GameStateController.PlayerType.COMPUTER);
        UIController.Instance.OpenGameSetupCanvas();
    }
	public void CloseGameSetup() {
        UIController.Instance.CloseGameSetupCanvas();
    }
    public void QuitGame() {
        Application.Quit();
    }
    #endregion

}