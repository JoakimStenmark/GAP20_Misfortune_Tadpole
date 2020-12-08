using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadpoleController : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void LateUpdate()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
    }
}
