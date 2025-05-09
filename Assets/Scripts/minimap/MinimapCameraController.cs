using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private LayerMask minimapLayer;
    [SerializeField]
    private LayerMask playerDotLayer;
    [SerializeField]
    private Camera miniCamera;

    private Vector3 originPos;
    private Vector3 playerOriginPos;



    void Awake()
    {
        originPos = transform.position;
        playerOriginPos = player.transform.position;
        miniCamera.cullingMask &= 0;
        miniCamera.cullingMask |= minimapLayer;
        miniCamera.cullingMask |= playerDotLayer;
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    void FixedUpdate()
    {
        gameObject.transform.position = 
        originPos - playerOriginPos + player.transform.position;
    }
}
