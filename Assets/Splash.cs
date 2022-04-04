using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    int LevelNumberPref
    {
        get { return SaveData.Instance.LevelNumberPref; }
        set
        {
            SaveData.Instance.LevelNumberPref = value;
            SaveSystem.SaveProgress(); 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(LevelNumberPref);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
