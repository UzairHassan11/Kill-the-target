using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTarget : MonoBehaviour
{
    public Animator levelTargetAnim;
    [SerializeField] bool levelTargetClickable = false;
    [SerializeField] private Sprite targetSprite;
    [SerializeField] private Image targetImage;

    private void Start()
    {
        targetImage.sprite = targetSprite;
        StartCoroutine(LevelTargetStartSequence());
    }

    #region level-target-stuff

    private IEnumerator LevelTargetStartSequence()
    {
        yield return new WaitForSeconds(.5f);
        levelTargetAnim.SetTrigger("Do");
        yield return new WaitForSeconds(1f);
        levelTargetAnim.SetTrigger("Do");
        yield return new WaitForSeconds(.5f);
        levelTargetClickable = true;
    }
    
    public void OnClickLevelTarget()
    {
        if (levelTargetClickable)
        {
            StartCoroutine(waitAndDisableLevelTarget());
        }
    }

    IEnumerator waitAndDisableLevelTarget()
    {
        levelTargetClickable = false;
        levelTargetAnim.SetTrigger("Do");
        yield return new WaitForSeconds(1f);
        levelTargetAnim.SetTrigger("Do");
        yield return new WaitForSeconds(1f);
        levelTargetClickable = true;
    }

    #endregion
}
