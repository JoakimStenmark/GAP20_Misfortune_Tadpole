using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    private Touch touch;
    private TouchPhase phase;

    public static TouchInput instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            phase = touch.phase;
        }
        else
            phase = TouchPhase.Canceled;
    }

    public bool TouchBegan()
    {
        bool beganTouch = phase == TouchPhase.Began ? true : false;
        return beganTouch;
    }

    public bool TouchContinue()
    {
        bool beganTouch = false;

        if (phase == TouchPhase.Moved || phase == TouchPhase.Stationary)
        {
            beganTouch = true;

        }
        return beganTouch;
    }
    public bool TouchEnded()
    {
        bool beganTouch = phase == TouchPhase.Ended ? true : false;
        return beganTouch;
    }
}
