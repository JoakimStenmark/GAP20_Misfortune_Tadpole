using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFlower : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("victory!");
    }

}
