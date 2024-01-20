using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void SetValue(uint value)
    {
        _slider.value = value;
    } 

    public void SetMinValue(uint minValue)
    {
        _slider.minValue = minValue;
    } 

    public void SetMaxValue(uint maxValue)
    {
        _slider.maxValue = maxValue;
    } 
}
