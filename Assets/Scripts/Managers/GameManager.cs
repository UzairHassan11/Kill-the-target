using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region vars

    public GameState currentGameState;

    public UiManager uiManager;

    public SniperController sniperController;
    #endregion

    #region singleton

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    #region unity

    // Start is called before the first frame update
    void Start()
    {

    }

    #endregion

    #region others

    public void ChangeGameState(GameState requestedState)
    {
        if (currentGameState == GameState.Idle && requestedState == GameState.Running)
        {
            currentGameState = requestedState;
            CameraManager.instance.SetAnimatorState(CamStates.gameplay);
        }
        else if (currentGameState == GameState.Running &&
                 requestedState == GameState.FinalMomentum)
        {
            currentGameState = requestedState;
            CameraManager.instance.SetAnimatorState(CamStates.finalMomentum);
        }
        else if (currentGameState == GameState.FinalMomentum &&
                 requestedState == GameState.Win)
        {
            currentGameState = requestedState;
            uiManager.ShowWinPanel(2);
            CameraManager.instance.SetAnimatorState(CamStates.endPoint);
        }
        else if (currentGameState == GameState.Running &&
                 requestedState == GameState.Fail)
        {
            currentGameState = requestedState;
            uiManager.ShowFailPanel();
            CameraManager.instance.SetAnimatorState(CamStates.endPoint);
        }
    }

    public void ChangeGameState(int index)
    {
        ChangeGameState((GameState) index);
    }
    #endregion
}

public enum GameState
{
    Idle,
    Running,
    FinalMomentum,
    Win,
    Fail
}