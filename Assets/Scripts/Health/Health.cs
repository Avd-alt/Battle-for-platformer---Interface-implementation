using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _maxHealth = 100;
    private float _minHealth = 0;
    private Coroutine _destroyCoroutine;

    public float CurrentHealth { get; private set; }

    public event Action Died;
    public event Action Hurt;
    public event Action ChangedHealth;

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    private void OnDestroy()
    {
        if (_destroyCoroutine != null)
        {
            StopCoroutine(_destroyCoroutine);
        }
    }

    public void TakeDamage(float damage)
    {
        if(damage > 0)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, _minHealth, _maxHealth);

            Hurt?.Invoke();
            ChangedHealth?.Invoke();

            if (CurrentHealth == _minHealth)
            {
                Die();
            }
        }
    }

    public void Heal(float amountHealthRestore)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amountHealthRestore, _minHealth, _maxHealth);

        ChangedHealth?.Invoke();
    }

    private void Die()
    {
        Died?.Invoke();
        _destroyCoroutine = StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        float delay = 5f;

        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}