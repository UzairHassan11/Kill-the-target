using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class WanderingHuman : MonoBehaviour
{
    #region vars

    public bool isTarget;

    // [SerializeField] private DOTweenPath _path;
    
    Transform camTrans;

    [SerializeField] private Vector3 deathCamPosition = new Vector3(0, 10, 0);

    private Animator animator;
    
    [HideInInspector]
    public NavMeshAgent agent;

    [HideInInspector] public bool died;
    
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    #region other-functions

    public void Run(float speed)
    {
        animator.SetFloat("Speed", 1);
        agent.speed = speed;
    }
    
    public void Walk(float speed)
    {
        animator.SetFloat("Speed", .5f);
        agent.speed = speed;
    }
    
    public void Idle()
    {
        animator.SetFloat("Speed", 0);
        agent.speed = 0;
    }

    void Die()
    {
        animator.SetTrigger("Die");
    }
    
    public void GotHit(Transform cam)
    {
        // _path.DOKill();
        Idle();
        Die();
        agent.radius = 0;
        died = true;
        AssignDeathCam(cam);
        ParticlesController.instance.SpawnParticle
            (ParticlesNames.BloodSplat, transform, 2,
                new Vector3(0, 0.026f, 0));
        SoundManager.Instance.PlaySound(3);
        SoundManager.Instance.PlayBGSound(4);
    }

    public void GotTargeted()
    {
        // _path.DOPause();
        Idle();
    }

    private void Update()
    {
        if (camTrans != null)
        {
            camTrans.LookAt(transform);
            camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, deathCamPosition, Time.timeScale * .05f);
        }
    }

    void AssignDeathCam(Transform cam)
    {
        camTrans = cam;
        camTrans.SetParent(transform);
        cam.Translate(transform.forward * -20);
        // cam.DOLocalMoveZ(-10, .5f).SetRelative(true).SetEase(Ease.Linear).OnComplete(() =>
        //     cam.DOLocalMove(deathCamPosition, 1.5f));
    }

    #endregion
}
