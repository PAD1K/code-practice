using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private static XpBar _xpBar;
    private static uint _minXp = 0; 
    private static uint _maxXp = 3; 
    private static uint _currentXp;
    private static uint _level = 0;


    private void Awake() 
    {
        _currentXp = _minXp;
        _xpBar.SetMinXp(_minXp);
        _xpBar.SetMaxXp(_maxXp);
    } 

    public static void AddXp(uint xpValue)
    {
        _currentXp += xpValue;
        
        if (_currentXp >= _maxXp) 
        {
            _currentXp = 0;
            _level++;
        }

        _xpBar.SetXp(_currentXp);
    }
}
