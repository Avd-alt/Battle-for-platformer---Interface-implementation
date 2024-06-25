using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _playerLayer;

    private float _damage = 15f;
    private Health _health;
    private Coroutine _coroutineAttack;

    public event Action Attacked;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        if(_coroutineAttack != null)
        {
            StopCoroutine(_coroutineAttack);
        }

        _coroutineAttack = StartCoroutine(Attacking());
    }

    private void OnEnable()
    {
        _health.Died += DisableComponentAtDeath;
    }

    private void OnDisable()
    {
        _health.Died -= DisableComponentAtDeath;
    }

    private void Attack()
    {
        Collider2D _playerHit = Physics2D.OverlapCircle(_attackPoint.position, _attackRange, _playerLayer);

        if (_playerHit != null)
        {
            if(_damage > 0)
            {
                Attacked?.Invoke();
                _playerHit.GetComponent<Health>().TakeDamage(_damage);
            }
        }
    }

    private IEnumerator Attacking()
    {
        float timeToDelay = 1.25f;
        var delay = new WaitForSeconds(timeToDelay);

        while (true)
        {
            Attack();
            yield return delay;
        }
    }

    private void DisableComponentAtDeath()
    {
        StopCoroutine(_coroutineAttack);
        enabled = false;
    }
}