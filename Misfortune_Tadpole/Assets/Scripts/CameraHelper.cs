using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHelper : MonoBehaviour
{
    private CinemachineVirtualCamera cameraToHelp;
    public GameObject player;
    public float playerVelocity;
    public float currentZoom;
    public float zoomRate;
    public AnimationCurve zoomCurve;
    private PlayerController playerController;
    private StickToSurface stickToSurface;

    void Start()
    {
        cameraToHelp = GetComponent<CinemachineVirtualCamera>();
        playerController = player.GetComponent<PlayerController>();
        stickToSurface = player.GetComponent<StickToSurface>();
    }

    void FixedUpdate()
    {
        playerVelocity = playerController.rb2d.velocity.magnitude;

        if (stickToSurface.stuck)
        {
            cameraToHelp.Follow = stickToSurface.transform.parent;
        }
        else
            cameraToHelp.Follow = player.transform;

        if (playerVelocity > 10 || stickToSurface.stuck)
        {
            currentZoom = Mathf.Clamp(currentZoom + Time.deltaTime * zoomRate, 0f, 1f);
        }
        else
            currentZoom = Mathf.Clamp(currentZoom - Time.deltaTime * zoomRate, 0f, 1f);

        cameraToHelp.m_Lens.OrthographicSize = zoomCurve.Evaluate(currentZoom);

    }
}
