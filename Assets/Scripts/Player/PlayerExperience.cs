using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private Bar _xpBar;
    private uint _minXp = 0; 
    private uint _maxXp = 3; 
    private uint _currentXp;
    private uint _level = 0;


    private void Awake() 
    {
        _currentXp = _minXp;
        _xpBar.SetValue(_currentXp);
        _xpBar.SetMinValue(_minXp);
        _xpBar.SetMaxValue(_maxXp);
    } 

    public void AddXp(uint xpValue)
    {
        _currentXp += xpValue;
        
        if (_currentXp >= _maxXp) 
        {
            _currentXp = 0;
            _level++;
        }

        _xpBar.SetValue(_currentXp);
    }
}
