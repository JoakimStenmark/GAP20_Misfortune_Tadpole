using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelBoundary : MonoBehaviour
{
    public GameObject cinemachineVCamObject;
    private CameraHelper cameraHelper;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cameraHelper = cinemachineVCamObject.GetComponent<CameraHelper>();
            cameraHelper.deadPlayer = true;

            Invoke(nameof(ResetScene), 1f);
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        cameraHelper.deadPlayer = false;
    }
}
