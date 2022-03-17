using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    #region singleton

    public static CameraManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    [SerializeField] Animator animator;

    public void SetAnimatorState(CamStates camState)
    {
        animator.CrossFade(camState.ToString(), .1f);
    }
    
    public void SetAnimatorState(int index)
    {
        animator.CrossFade(((CamStates)index).ToString(), .1f);
    }
    
    public void SetAnimatorState(string state)
    {
        animator.CrossFade(state, .1f);
    }
}
[System.Serializable]
public enum CamStates
{
    start,
    gameplay,
    endPoint,
    finalMomentum
}