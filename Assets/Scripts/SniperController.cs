using UnityEngine;

public class SniperController : MonoBehaviour
{
    #region vars

    [SerializeField] private Transform gunTransform, zoomIn, zoomOut;

    private Camera cam;

    [SerializeField] private float cameraTransitionSpeed;

    private bool zoomedIn, fullyZoomed;

    [SerializeField] private GameObject zoomOutCrossheir, zoomInCrossheir, gunMesh;

    private ControllerState controllerState;

    [SerializeField] private Vector2 fovLimits;

    [SerializeField] private Transform bulletSpawnPoint;

    private Vector2 inputDelta;
    [SerializeField] Vector2 camX_Limits, camY_Limits;
    public float moveSpeed;

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // if (controllerState == ControllerState.Idle)
            // {
            //     controllerState = ControllerState.Aiming;
            //     // controlling = true;
            //     mouseLastPosition = Input.mousePosition;
            // }
        }
        
        HandleZooming();

        HandleCameraValues();
        
        zoomOutCrossheir.SetActive(!fullyZoomed);
        
        gunMesh.SetActive(!fullyZoomed);
        
        zoomInCrossheir.SetActive(fullyZoomed);

        // joystick.enabled = fullyZoomed;
        
        if(fullyZoomed)
            HandleCameraMovement();
    }

    void HandleZooming()
    {
        if (Input.GetMouseButtonDown(0))
        {
            zoomedIn = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            zoomedIn = false;
        }
    }

    void HandleCameraValues()
    {
        // cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomedIn ? zoomIn.FOV : zoomOut.FOV,
        //     Time.deltaTime * cameraTransitionSpeed);
        gunTransform.transform.position = Vector3.Lerp(gunTransform.transform.position,
            zoomedIn ? zoomIn.position : zoomOut.position,
            Time.deltaTime * cameraTransitionSpeed);
        gunTransform.transform.rotation = Quaternion.Lerp(gunTransform.transform.rotation,
            zoomedIn ? zoomIn.rotation : zoomOut.rotation,
            Time.deltaTime * cameraTransitionSpeed);

        fullyZoomed = Vector3.Distance(gunTransform.transform.position, zoomIn.position) < 0.05f;

        cam.fieldOfView =
            Mathf.Lerp(cam.fieldOfView, Input.GetMouseButton(0) ? fovLimits.x : fovLimits.y,
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

    private Vector2 mouseDelta;

    void SpawnBullet()
    {
        
    }
}
public enum ControllerState
{
    Idle,
    Aiming,
    Shot_Fired,     // means bullet is airborn
    Shot_Hit_Target,
    Shot_Hit_NPC,
    Shot_Missed,
    Level_Win,
    GameOver
}