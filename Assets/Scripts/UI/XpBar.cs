using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Awake() 
    {
        _slider.value = 0;
    }
    public void SetXp(uint xpValue)
    {
        _slider.value = xpValue;
    } 

    public void SetMaxXp(uint maxXpValue)
    {
        _slider.maxValue = maxXpValue;
    } 

    public void SetMinXp(uint minXpValue)
    {
        _slider.minValue = minXpValue;
    } 
}
