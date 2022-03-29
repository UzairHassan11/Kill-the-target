using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public static Crosshair Instance;
    private GameObject crosshair; 
    [SerializeField] private Canvas canvas;
    
    private RaycastHit HitInfo;
    [SerializeField] private GameObject crosshairAestheticObj;
    private Image crosshairAestheticObjImage;
    public Vector3 hitPosition;
   
    float pixelX;
    float pixelY;
    float posX;
    float posY;
    public Transform hitIndicator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }
    
    void Start()
    {
        crosshair = this.gameObject;
        crosshairAestheticObjImage = crosshairAestheticObj.GetComponent<Image>();
        pixelX = canvas.pixelRect.width;
        pixelY = canvas.pixelRect.height;
    }
    
    void Update()
    {
        //if(GameManager.Instance.GameState == GameState.Aim_Idle)
        // {
            CheckRay();
        // }
    }

    void CheckRay()
    {
        Ray ray = Camera.main.ViewportPointToRay ( new Vector3(0.5f,0.5f,0));
        Debug.DrawRay(ray.origin, ray.direction * 10000, Color.yellow);
        if (Physics.Raycast(ray, out HitInfo))
        {
            if (HitInfo.collider != null)
            {
                hitPosition = HitInfo.point;
                hitIndicator.position = hitPosition;
            }
        }
    }
}
