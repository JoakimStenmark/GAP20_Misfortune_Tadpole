using UnityEngine;
using UnityEngine.UI;


public class WaterBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxWater(float water)
    {
        slider.maxValue = water;
    }

    public void SetWater(float water)
    {
        slider.value = water;
    }
}
