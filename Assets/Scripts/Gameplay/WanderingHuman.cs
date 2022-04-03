using DG.Tweening;
using UnityEngine;

public class WanderingHuman : MonoBehaviour
{
    public bool isTarget;

    [SerializeField] private DOTweenPath _path;
    
    Transform camTrans;

    [SerializeField] private Vector3 deathCamPosition;
    
    public void GotHit(Transform cam)
    {
        _path.DOKill();
        AssignDeathCam(cam);
    }

    public void GotTargeted()
    {
        _path.DOPause();
    }

    private void Update()
    {
        if(camTrans!=null)
            camTrans.LookAt(transform);
    }

    void AssignDeathCam(Transform cam)
    {
        camTrans = cam;
        camTrans.SetParent(transform);
        // cam.Translate(transform.forward * -20);
        cam.DOLocalMoveZ(-10, .5f).SetRelative(true).SetEase(Ease.Linear).OnComplete( ()=>
            cam.DOLocalMove(deathCamPosition, 1.5f));
    }
}
