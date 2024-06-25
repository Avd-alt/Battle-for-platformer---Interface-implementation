using UnityEngine;

public class PlayerDetecter : MonoBehaviour
{
    private Health _healthEnemy;

    public Health PlayerHealth { get; private set; }

    private void Awake()
    {
        _healthEnemy = GetComponentInParent<Health>();
    }

    private void OnEnable()
    {
        _healthEnemy.Died += DisableComponentAtDeath;
    }

    private void OnDisable()
    {
        _healthEnemy.Died -= DisableComponentAtDeath; 
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.TryGetComponent(out Health playerHealth))
        {
            PlayerHealth = playerHealth;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Health playerHealth))
        {
            PlayerHealth = null;
        }
    }

    private void DisableComponentAtDeath()
    {
        enabled = false;
    }
}