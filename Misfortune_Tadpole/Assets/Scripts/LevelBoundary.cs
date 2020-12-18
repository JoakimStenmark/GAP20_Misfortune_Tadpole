using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelBoundary : MonoBehaviour
{
    public float respawnTime;
    private CameraHelper cameraHelper;
    private PauseMenuManager pauseMenuManager;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pauseMenuManager = GameObject.FindGameObjectWithTag("UI").GetComponent<PauseMenuManager>();

            cameraHelper = GameObject.FindGameObjectWithTag("CM vcam").GetComponent<CameraHelper>();
            cameraHelper.deadPlayer = true;

            Invoke(nameof(ResetScene), respawnTime);
        }
    }

    void ResetScene()
    {
        pauseMenuManager.ReloadScene();
        cameraHelper.deadPlayer = false;
    }
}
