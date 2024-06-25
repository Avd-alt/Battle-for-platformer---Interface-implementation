using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(PlayerInput))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _enemyLayers;

    private float _oneSecondTime = 1f;
    private int _attackDamage = 20;
    private float _attackRate = 1f;
    private float _nextAttackTime = 0;
    private Health _playerHealth;
    private PlayerInput _playerInput;

    public event Action Attacked;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _playerHealth.Died += DisableComponentAtDeath;
    }

    private void OnDisable()
    {
        _playerHealth.Died -= DisableComponentAtDeath;
    }

    private void Update()
    {
        if(Time.time >= _nextAttackTime)
        {
            if (_playerInput.IsTryingAttack)
            {
                Attack();

                _nextAttackTime = Time.time + _oneSecondTime/ _attackRate;
            }
        }

        _playerInput.DeActivateAttackTrying();
    }

    private void Attack()
    {
        Attacked?.Invoke();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy != null)
            {
                if(_attackDamage > 0)
                {
                    enemy.GetComponent<Health>().TakeDamage(_attackDamage);
                }
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (Time.time >= _nextAttackTime)
            {
                Attack();
                _nextAttackTime = Time.time + _oneSecondTime / _attackRate;
            }
            yield return null;
        }
    }

    private void DisableComponentAtDeath()
    {
        enabled = false;
    }
}