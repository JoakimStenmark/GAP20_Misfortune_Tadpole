using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHelper : MonoBehaviour
{
    private CinemachineVirtualCamera cameraToHelp;
    public float maxOrthographicSize;
    public Rigidbody2D playerRb2d;
    public float playerVelocity;
    public float currentZoom;
    public float zoomRate;
    public AnimationCurve zoomCurve;

    void Start()
    {
        cameraToHelp = GetComponent<CinemachineVirtualCamera>();
    }

    void FixedUpdate()
    {   
        playerVelocity = playerRb2d.velocity.magnitude;

        if (playerVelocity > 10)
        {
            currentZoom = Mathf.Clamp(currentZoom + Time.deltaTime * zoomRate, 0f, 1f);
        }
        else
            currentZoom = Mathf.Clamp(currentZoom - Time.deltaTime * zoomRate, 0f, 1f);

        cameraToHelp.m_Lens.OrthographicSize = zoomCurve.Evaluate(currentZoom);

    }
}
