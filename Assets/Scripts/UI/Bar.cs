using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    protected Slider _slider;

    public Bar(Slider slider) 
    {
        _slider = slider;
    }

    public void Setvalue(uint value)
    {
        _slider.value = value;
    } 

    public void SetMinValue(uint minValue)
    {
        _slider.minValue = minValue;
        _slider.value = minValue;
    } 

    public void SetMaxValue(uint maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = maxValue;
    } 
}
