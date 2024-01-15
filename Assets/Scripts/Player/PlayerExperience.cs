using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private XpBar _xpBar;
    private int _minXp = 0; 
    private int _maxXp = 3; 
    private int _currentXp;
    private int _level = 0;


    private void Awake() 
    {
        _currentXp = _minXp;
        _xpBar.SetMinXp(_minXp);
        _xpBar.SetMaxXp(_maxXp);
    } 

    public void AddXp(int xpValue)
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
