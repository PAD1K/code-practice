using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private int _attackDamage = 40;
    [SerializeField] private float _attackRate = 2f;
    [SerializeField] private float _nextAttackTime = 0f;
    [SerializeField] private uint _maxHealth = 100;
    [SerializeField] private uint _minHealth = 0;
    [SerializeField] private uint _currentHealth;
    [SerializeField] private Bar _healtBar;
    
    private PlayerMovement _playerMovement;
    private PlayerInputs _input;

    private void Awake() 
    {
        _input = new PlayerInputs();
        _input.Enable();

        _input.Player.Attack.performed += context => Attack();
        
        
        _currentHealth = _maxHealth;
        _healtBar.SetMaxValue(_maxHealth);
        _healtBar.SetMinValue(_minHealth);
        _healtBar.SetValue(_maxHealth);
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Attack()
    {
        if (Time.time >= _nextAttackTime && !_animator.GetBool("IsJumping")) 
        {
            _animator.SetTrigger("Attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);
        
            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(_attackDamage);
            }

            _playerMovement.DecreaseStamina();

            _nextAttackTime = Time.time + 1f / _attackRate;
        }
    }

    private void OnDrawGizmosSelected() 
    {
        if (_attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    private void TakeDamage(uint damage)
    {
        _currentHealth -= damage;

        _healtBar.SetValue(_currentHealth);
    }
}