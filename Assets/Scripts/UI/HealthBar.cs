using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Bar
{
    [SerializeField] private Slider _slider;

    public HealthBar(Slider slider) : base(slider)
    {
        base._slider = _slider;
    }
}