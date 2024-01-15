using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _currentHealth;
    private PlayerExperience _playerExperience;

    void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int countOfDamage)
    {
        _currentHealth -= countOfDamage;
        
        // TODO:
        // Add hurt animation.

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerExperience>().AddXp(1);
        Destroy(gameObject);
        
        // TODO:
        // Add die animation.
    }
}
