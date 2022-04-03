using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private Transform bulletMesh, cam, tempTrans;
    [SerializeField] private GameObject rotatingTrail, windParticles;
    private Tween tween;
    private bool targetIsHuman;

    public void Spawn(Vector3 targetPosition, bool targetIsHuman = false)
    {
        this.targetIsHuman = targetIsHuman;
        
        transform.DOMove(targetPosition, targetIsHuman ? moveSpeed : 200).SetEase(Ease.Linear).SetSpeedBased(true)
            .OnComplete(BulletHit);

        gameObject.SetActive(true);
        rotatingTrail.SetActive(targetIsHuman);
        windParticles.SetActive(targetIsHuman);
        // tempTrans.position = targetPosition;

        if (targetIsHuman)
        {
            cam.gameObject.SetActive(true);

            tween = bulletMesh.DOLocalRotate(new Vector3(0, 0, 360), .5F)
                .SetRelative(true).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
    }

    // private void Update()
    // {
    //     if(cam.gameObject.activeSelf)
    //         cam.transform.LookAt(bulletMesh);
    // }

    void BulletHit()
    {
        if (!cam.gameObject.activeSelf)
        {
            BulletHitDelayedAction();
            return;
        }
        
        Invoke("BulletHitDelayedAction", .1f);
        if (tween != null)
            tween.Kill();
        // cam.DOShakePosition(.25f, Vector3.one, 10);
        rotatingTrail.SetActive(false);
        windParticles.SetActive(false);
    }

    void BulletHitDelayedAction()
    {
        // Time.timeScale = 1;
        gameObject.SetActive(false);
        if(!targetIsHuman)
            GameManager.instance.sniperController.SetStateIdle();
        else
        {
            GameManager.instance.sniperController.HitHuman(cam);
        }
    }
}
