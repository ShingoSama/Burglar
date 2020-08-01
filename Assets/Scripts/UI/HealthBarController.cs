using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider slider;
    public void SetMaxHealth(float maxHeal)
    {
        slider.maxValue = maxHeal;
        slider.value = maxHeal;
    }
    public void SetHealth (float currentHeal)
    {
        slider.value = currentHeal;
    }
}
