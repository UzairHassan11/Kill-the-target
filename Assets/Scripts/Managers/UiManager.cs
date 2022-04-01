using System.Collections;
// using ElephantSDK;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using GameAnalyticsSDK;

public class UiManager : MonoBehaviour
{
    #region .
    int LevelNumberPref
    {
        get { return SaveData.Instance.LevelNumberPref; }
        set
        {
            SaveData.Instance.LevelNumberPref = value;
            SaveSystem.SaveProgress(); 
        }
    }
    
    int LevelNumberAnalytics
    {
        get { return SaveData.Instance.LevelNumberAnalytics; }
        set
        {
            SaveData.Instance.LevelNumberAnalytics = value;
            SaveSystem.SaveProgress(); 
        }
    }
    #endregion

    [SerializeField] private Text levelText;
    
    [SerializeField] private bool test;
    
    [SerializeField] private GameObject startPanel, winPanel, failPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = "Level " + LevelNumberAnalytics.ToString("00");
        if (!test)
        {
            // Elephant.LevelStarted(LevelNumberAnalytics);
            // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "LevelStart", LevelNumberAnalytics);
        }
    }


    public void OnClickStartPanel()
    {
        startPanel.SetActive(false);
        GameManager.instance.ChangeGameState(GameState.Running);
        if (!test)
        {
            GameManager.instance.ChangeGameState(GameState.Running);
        }
    }

    public void ShowWinPanel(float delay = 0)
    {
        if (delay > 0)
        {
            StartCoroutine(showPanelWithDelay(delay, true));
            return;
        }
        
        if (!test)
        {
            // Elephant.LevelCompleted(LevelNumberAnalytics);
            // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level_Completed", LevelNumberAnalytics);
        }

        winPanel.SetActive(true);
        LevelNumberAnalytics++;
        LevelNumberPref++;
    }
    
    public void ShowFailPanel(float delay = 0)
    {
        if (delay > 0)
        {
            StartCoroutine(showPanelWithDelay(delay, false));
            return;
        }
        
        if (!test)
        {
            // Elephant.LevelFailed(LevelNumberAnalytics);
            // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level_Failed", LevelNumberAnalytics);
        }

        failPanel.SetActive(true);
    }

    IEnumerator showPanelWithDelay(float delay, bool win)
    {
        yield return new WaitForSeconds(delay);
        if (win) 
            ShowWinPanel(0);
        else
            ShowFailPanel(0);
    }

    public void OnReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
