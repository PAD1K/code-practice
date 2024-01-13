using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _currentHealth;
    void Start()
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
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;        
        this.enabled = false;

        // Destroy(gameObject);
        
        // TODO:
        // Add die animation.
    }
}
