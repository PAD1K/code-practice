using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void SetStaminaValue(int stamina)
    {
        _slider.value = stamina;
    } 

    public void SetMaxStaminaValue(int maxStamina)
    {
        _slider.maxValue = maxStamina;
        _slider.value = maxStamina;
    } 
}
