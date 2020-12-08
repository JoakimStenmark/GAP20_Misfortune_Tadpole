using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadpoleController : MonoBehaviour
{
    
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
