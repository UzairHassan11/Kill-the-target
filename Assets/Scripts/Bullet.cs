using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool canMove;
    private Vector3 targetPosition;
    [SerializeField] private Transform cameraTransform;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Spawn(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        canMove = true;
    }
    
    private void Update()
    {
        if(canMove)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 
                30f * Time.deltaTime);
    }

    // Bullet hit chaser (target), hitScore++
    public void BulletHitChaser()
    {
        rigidbody.isKinematic = true;
        canMove = false;
        cameraTransform.localPosition = new Vector3(-5f, 15f, -10f);
    }
    
    // Bullet hit escaper, gameover
    public void BulletHitEscaper()
    {
        rigidbody.isKinematic = true;
        canMove = false;
        cameraTransform.localPosition = new Vector3(-5f, 15f, -10f);
    }
}
