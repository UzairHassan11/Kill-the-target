using UnityEngine;

public class SniperController : MonoBehaviour
{
    #region vars

    [SerializeField] private Transform gunTransform, zoomIn, zoomOut;

    private Camera cam;

    [SerializeField] private float cameraTransitionSpeed;

    [SerializeField] bool zoomedIn, fullyZoomed;

    [SerializeField] private GameObject zoomOutCrossheir, zoomInCrossheir, gunMesh;

    [SerializeField] private Vector2 fovLimits;

    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] Vector2 camX_Limits, camY_Limits;
    public float moveSpeed;

    [SerializeField] private ControllerState controllerState;

    [SerializeField] private Animator gunAnimator;

    [SerializeField] private SniperAnimator _sniperAnimator;
    
    private RaycastHit HitInfo;

    private Vector3 basePosition;
    
    private Vector2 mouseDelta;

    private WanderingHuman currentHuman;
    #endregion

    #region unity

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        basePosition = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();

        HandleCameraFOV();
        
        HandleGunMovement();
        
        zoomOutCrossheir.SetActive(controllerState == ControllerState.Aiming && !fullyZoomed);
        
        gunMesh.SetActive(!fullyZoomed);
        
        zoomInCrossheir.SetActive(fullyZoomed);

        fullyZoomed = Vector3.Distance(gunTransform.transform.position, zoomIn.position) < 0.05f;

        if (controllerState == ControllerState.Aiming)
        {
            CheckRay();

            if(fullyZoomed)
                HandleCameraMovement();
        }
    }
    

    #endregion
    
    #region custom-functions

    void CheckRay()
    {
        Ray ray = Camera.main.ViewportPointToRay ( new Vector3(0.5f,0.5f,0));
        Debug.DrawRay(ray.origin, ray.direction * 10000, Color.yellow);
        Physics.Raycast(ray, out HitInfo);
    }

    void HandleInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(controllerState != ControllerState.Idle)
                return;
            
            zoomedIn = true;
            controllerState = ControllerState.Aiming;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (controllerState != ControllerState.Aiming)
                return;
            
            zoomedIn = false;
            if (fullyZoomed)
            {
                // Time.timeScale = .1f;

                SpawnBullet();
                fullyZoomed = false;
            }
            else
            {
                controllerState = ControllerState.Idle;
            }

            cam.transform.position = basePosition;
        }
    }

    void HandleGunMovement()
    {
        // cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomedIn ? zoomIn.FOV : zoomOut.FOV,
        //     Time.deltaTime * cameraTransitionSpeed);
        gunTransform.transform.position = Vector3.Lerp(gunTransform.transform.position,
            zoomedIn ? zoomIn.position : zoomOut.position,
            Time.deltaTime * cameraTransitionSpeed);
        gunTransform.transform.rotation = Quaternion.Lerp(gunTransform.transform.rotation,
            zoomedIn ? zoomIn.rotation : zoomOut.rotation,
            Time.deltaTime * cameraTransitionSpeed);
    }

    void HandleCameraFOV()
    {
        // cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomedIn ? zoomIn.FOV : zoomOut.FOV,
        //     Time.deltaTime * cameraTransitionSpeed);
        gunTransform.transform.position = Vector3.Lerp(gunTransform.transform.position,
            zoomedIn ? zoomIn.position : zoomOut.position,
            Time.deltaTime * cameraTransitionSpeed);
        gunTransform.transform.rotation = Quaternion.Lerp(gunTransform.transform.rotation,
            zoomedIn ? zoomIn.rotation : zoomOut.rotation,
            Time.deltaTime * cameraTransitionSpeed);
        
        cam.fieldOfView =
            Mathf.Lerp(cam.fieldOfView, controllerState == ControllerState.Aiming ? fovLimits.x : fovLimits.y,
                Time.deltaTime * cameraTransitionSpeed);
    }
    
    void HandleCameraMovement()
    {
        // return;
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseDelta *= Time.deltaTime;
        mouseDelta *= moveSpeed;
        cam.transform.position += new Vector3(mouseDelta.x, mouseDelta.y, 0);
       
        cam.transform.position = new Vector3(
            Mathf.Clamp(cam.transform.position.x, camX_Limits.x, camX_Limits.y),
            Mathf.Clamp(cam.transform.position.y, camY_Limits.x, camY_Limits.y),
            cam.transform.position.z);
    }

    void SpawnBullet()
    {
        bool targetIsHuman = HitInfo.transform.GetComponent<WanderingHuman>() != null;
        
        if (targetIsHuman)
        {
            currentHuman = HitInfo.transform.GetComponent<WanderingHuman>();
            currentHuman.GotTargeted();
            controllerState = ControllerState.Shot_Fired;
        }
        else
        {
            print("0");
            ReloadGun();
        }
        Instantiate(bulletPrefab).Spawn(HitInfo.point, targetIsHuman);
    }

    void ReloadGun()
    {
        print("1");
        controllerState = ControllerState.Reloading;
        // gunAnimator.SetTrigger("New Trigger");
        _sniperAnimator.SetTrigger("reload");

        Invoke("SetStateIdle", 1);
    }

    public void SetStateIdle()
    {
        controllerState = ControllerState.Idle;
    }

    public void HitHuman()
    {
        currentHuman.GotHit();
        if(currentHuman.isTarget)
            GameManager.instance.uiManager.ShowWinPanel();
        else
            GameManager.instance.uiManager.ShowFailPanel();
    }
    #endregion
}
#region ControllerState
public enum ControllerState
{
    Idle,
    Aiming,
    Shot_Fired,
    Reloading
}
#endregion