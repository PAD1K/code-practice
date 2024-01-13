using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private int _attackDamage = 40;
    [SerializeField] private float _attackRate = 2f;
    [SerializeField] private float _nextAttackTime = 0f;

    private PlayerInputs _input;

    private void Awake() 
    {
        _input = new PlayerInputs();
        _input.Enable();

        _input.Player.Attack.performed += context => Attack();
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
}
