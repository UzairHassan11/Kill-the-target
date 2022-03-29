using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private Transform meshes;

    public void Spawn(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, moveSpeed).SetEase(Ease.Linear).SetSpeedBased(true).onComplete = BulletHit;
        gameObject.SetActive(true);
    }

    void BulletHit()
    {
        gameObject.SetActive(false);
    }
}
