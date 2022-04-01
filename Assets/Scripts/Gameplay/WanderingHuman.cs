using DG.Tweening;
using UnityEngine;

public class WanderingHuman : MonoBehaviour
{
    public bool isTarget;

    [SerializeField] private DOTweenPath _path;

    public void GotHit()
    {
        _path.DOKill();
    }

    public void GotTargeted()
    {
        _path.DOPause();
    }
}
