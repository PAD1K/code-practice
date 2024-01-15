using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void SetHealth(int healthValue)
    {
        _slider.value = healthValue;
    } 

    public void SetMaxHealth(int maxHealthValue)
    {
        _slider.maxValue = maxHealthValue;
        _slider.value = maxHealthValue;
    } 
}
