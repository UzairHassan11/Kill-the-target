using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private Transform bulletMesh, cam, tempTrans;
    [SerializeField] private GameObject rotatingTrail, windParticles;
    private Tween tween;
    
    public void Spawn(Vector3 targetPosition, float duration = 5)
    {
        transform.DOMove(targetPosition, moveSpeed).SetEase(Ease.Linear).SetSpeedBased(true).onComplete = BulletHit;
        gameObject.SetActive(true);
        tempTrans.position = targetPosition;
        // bulletMesh.transform.LookAt(tempTrans);

        // bulletMesh.rotation.SetLookRotation(transform.position - targetPosition - transform.position, transform.up);
        tween = bulletMesh.DOLocalRotate(new Vector3(0, 0, 360), .5F).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    // private void Update()
    // {
    //     if(cam.gameObject.activeSelf)
    //         cam.transform.LookAt(bulletMesh);
    // }

    void BulletHit()
    {
        Invoke("BulletHitDelayedAction", .25f);
        tween.Kill();
        // cam.DOShakePosition(.25f, Vector3.one, 10);
        rotatingTrail.SetActive(false);
        windParticles.SetActive(false);
    }

    void BulletHitDelayedAction()
    {
        gameObject.SetActive(false);
        GameManager.instance.sniperController.SetStateIdle();
    }
}
